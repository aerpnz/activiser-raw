
GetTimeStamp = function () {
    return new Number(new Date().getTime());
}

var utc = new Date(2013,4,27,21,50,24);
var local = new Date(2013,4,28,09,50,24);
var supplied = new Date(1369648224000);

//new Date()getTime()

WScript.echo(utc);
WScript.echo(local);
WScript.echo(supplied);

WScript.echo(utc.getTime());
WScript.echo(local.getTime());
WScript.echo(supplied.getTime());

WScript.echo(utc.toUTCString());
WScript.echo(local.toUTCString());
WScript.echo(supplied.toUTCString());

WScript.echo(GetTimeStamp());