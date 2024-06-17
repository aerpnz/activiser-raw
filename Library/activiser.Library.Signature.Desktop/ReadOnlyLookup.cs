using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace activiser.Library
{
    class ReadOnlyLookup : LookupBase
    {
        private TextBox _textbox;

        ReadOnlyLookup() 
        {
            _textbox = new TextBox();
            _textbox.ReadOnly = true;
            _textbox.Dock = DockStyle.Fill;

            this.Controls.Add(_textbox);
        }
    }
}
