
var sig = {};

sig.ApplicationGuid = []; // uuid.parse("F97EB4E7-F684-4402-895D-CFD15CE86498");

sig.VersionNo = "#V040#";

sig.Base64Table =
[
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
    'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'
];


sig.sixbit2char = function (b) {
    if ((b >= 0) && (b <= 63)) {
        return sig.Base64Table[b];
    }
    if (b > 63) return sig.Base64Table[63]; // strictly an error, but we'll just handle it this way
    return sig.Base64Table[0];
}

sig.char2sixbit = function (c) {
    for (x = 0; x < 64; x++) {
        if (sig.Base64Table[x] == c)
            return x;
    }
    return 0; // strictly an error, but we'll just handle it this way
}

sig.int18ToBase64 = function (b) {
    var b1, b2, b3;
    b1 = ((b & 0x3F));
    b2 = ((b & (0x3F << 6)) >> 6);
    b3 = ((b & (0x3F << 12)) >> 12);

    return [sig.sixbit2char(b1) + sig.sixbit2char(b2) + sig.sixbit2char(b3)];
}

sig.int18FromBase64 = function (c) {
    if (c.Length != 3) { throw new Error("int18FromBase64 Input Invalid"); }
    var b1, b2, b3;
    b1 = sig.char2sixbit(c[0]);
    b2 = sig.char2sixbit(c[1]);
    b3 = sig.char2sixbit(c[2]);
    var result = b1 | (b2 << 6) | (b3 << 12);
    return result;
}

sig.GetTimeStamp = function () {
    var baseTime = 621355968000000000; // ticks between 1 Jan, 0000 and 1 Jan 1970.
    var now = new Date(); 
    var utc = new Date(now.getTime() + now.getTimezoneOffset() * 60000);

    return new Number((utc * 10000) + baseTime);
}


/*
M104,66 L104,67 L104,70 L103,73 L100,76 L96,79 L93,83 L88,84 L85,86 L82,87 L80,87 L78,87 L76,87 L75,86 L74,84 L72,83 L71,80 L70,79 L68,78 L67,75 L67,72 L65,69 L65,64 L66,59 L68,54 L69,50 L71,47 L72,47 L73,46 L75,51 L75,56 L77,63 L80,68 L84,75 L88,83 L91,88 L95,93 L98,97 L99,97 L100,97 L102,97 L103,95 L105,91 L106,90 L107,89 L107,88 L109,88 L110,91 L115,94 L119,97 L122,99 L125,101 L128,102 L131,102 L132,102 L134,102 L136,102 L140,102 L143,100 L144,99 L147,99 L150,97 L151,97 L153,97 L154,97 L154,99 L155,100 L157,101 L158,103 L159,103 L160,103 L162,103 L165,102 L168,101 L171,97 L175,92 L176,89 L180,84 L181,83 L181,81 L181,82 L181,86 L181,91 L181,98 L181,103 L181,108 L181,113 L181,116 L178,121 L175,122 L168,126 L161,128 L151,130 L141,130 L132,130 L125,130 L115,129 L110,125 L103,120 L97,114 L94,109 L90,102 L89,97 L87,92 L87,87 L85,82 L85,80 L84,79 L84,81 L84,86 L84,91 L84,96 L84,103 L80,108 L77,115 L73,118 L68,124 L65,125 L62,125 L59,125 L58,125 L56,124 L55,121 L52,117 L50,114 L50,111 L49,110 L49,112 L53,115 L58,121 L63,126 L71,132 L79,136 L89,140 L97,144 L107,146 L112,146 L117,146 L120,146 L123,145 L124,145 L125,144 L127,142 L128,142 L129,142 L131,141 L133,141 L135,141 L138,141 L139,141 L140,141 L143,141 L144,141 L147,141 L150,141 L153,141 L154,140 L156,140
*/

sig.convertSvgToActiviser = function (svgData) {
    var points = svgData.split(" ");

    var i, l, p;
    var line = null
    var lines = new Array();
    l = 0;
    for (i = 0; i < points.length; i++) {
        var type = points[i].substr(0, 1);
        var xy = points[i].substr(1).split(",");
        var x = parseInt(xy[0], 10) & 0xFF, y = parseInt(xy[1]) & 0x3FF;
        var xy18bit = sig.int18ToBase64(x << 8 | y);

        if (type == "M" || p > 62) {
            if (line != null) {
                lines[l] = line;
                l++;
                if (l > 63) break; // panic - too big.
            }
            line = [];
            p = 0;
            line[p] = xy18bit;
        }
        else if (type == "L") {
            if (line == null) {
                line = [];
                p = 0;
            }
            else {
                p++;
            }
            line[p] = xy18bit;
        }
        else {
            // Panic ?
        }
    }

    // wrap up

    if (line != null) lines[l] = line;



    var result = sig.VersionNo;
    result += sig.sixbit2char(lines.length);
    result += btoa(sig.GetTimeStamp()) + ";"


    for (l = 0; l < lines.length; l++) {
        result += sig.sixbit2char(lines[l].length);
    }

    result += "="; 

    for (l = 0; l < lines.length; l++) {
        for (p = 0; p < lines[l].length; p++) {
            result += lines[l][p];
        }
    }
    result += "=";

    return result;
}

