Module ActiviserSqlTypeMapping

    Friend Enum SqlSysType
        [bigint] = 127
        [binary] = 173
        [bit] = 104
        [char] = 175
        [date] = 40
        [datetime] = 61
        [datetime2] = 42
        [datetimeoffset] = 43
        [decimal] = 106
        [float] = 62
        [geography] = 130
        [geometry] = 129
        [hierarchyid] = 128
        [image] = 34
        [int] = 56
        [money] = 60
        [nchar] = 239
        [ntext] = 99
        [numeric] = 108
        [nvarchar] = 231
        [ObjectName] = 260
        [real] = 59
        [smalldatetime] = 58
        [smallint] = 52
        [smallmoney] = 122
        [sql_variant] = 98
        [sysname] = 256
        [text] = 35
        [time] = 41
        [timestamp] = 189
        [tinyint] = 48
        [uniqueidentifier] = 36
        [varbinary] = 165
        [varchar] = 167
        [xml] = 241
    End Enum

    Friend Function GetSqlDbType(ByVal value As SqlSysType) As Data.DbType
        Select Case value
            Case SqlSysType.bigint
                Return DbType.Int64
            Case SqlSysType.binary
                Return DbType.Binary
            Case SqlSysType.bit
                Return DbType.Boolean
            Case SqlSysType.char
                Return DbType.String
            Case SqlSysType.date
                Return DbType.Date
            Case SqlSysType.datetime
                Return DbType.DateTime
            Case SqlSysType.datetime2
                Return DbType.DateTime2
            Case SqlSysType.datetimeoffset
                Return DbType.DateTimeOffset
            Case SqlSysType.decimal
                Return DbType.Decimal
            Case SqlSysType.float
                Return DbType.Double
            Case SqlSysType.geography
                Return DbType.Object
            Case SqlSysType.geometry
                Return DbType.Object
            Case SqlSysType.hierarchyid
                Return DbType.Object
            Case SqlSysType.image
                Return DbType.Object
            Case SqlSysType.int
                Return DbType.Int32
            Case SqlSysType.money
                Return DbType.Currency
            Case SqlSysType.nchar
                Return DbType.String
            Case SqlSysType.ntext
                Return DbType.String
            Case SqlSysType.numeric
                Return DbType.Decimal
            Case SqlSysType.nvarchar
                Return DbType.String
            Case SqlSysType.ObjectName
                Return DbType.String
            Case SqlSysType.real
                Return DbType.Single
            Case SqlSysType.smalldatetime
                Return DbType.DateTime
            Case SqlSysType.smallint
                Return DbType.Int16
            Case SqlSysType.smallmoney
                Return DbType.Currency
            Case SqlSysType.sysname
                Return DbType.String
            Case SqlSysType.text
                Return DbType.String
            Case SqlSysType.time
                Return DbType.Time
            Case SqlSysType.timestamp
                Return DbType.Int64
            Case SqlSysType.tinyint
                Return DbType.Byte
            Case SqlSysType.uniqueidentifier
                Return DbType.Guid
            Case SqlSysType.varbinary
                Return DbType.Object
            Case SqlSysType.varchar
                Return DbType.String
            Case SqlSysType.xml
                Return DbType.Xml
            Case SqlSysType.sql_variant
                Return DbType.Object
        End Select
    End Function

End Module
