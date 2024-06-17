using MSForms = System.Windows.Forms;

namespace activiser.Library.Forms
{
    public sealed class Clipboard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static string GetText()
        {
            MSForms.IDataObject o = MSForms.Clipboard.GetDataObject();
            if (o != null && o.GetDataPresent(MSForms.DataFormats.StringFormat))
            {
                return (string)o.GetData(MSForms.DataFormats.StringFormat, true);
            }
            else
            {
                return string.Empty;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static void SetText(string value)
        {
            MSForms.Clipboard.SetDataObject(value);
        }

        private Clipboard()
        {

        }
    }
}
