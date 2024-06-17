namespace activiser.Library.activiserWebService
{
    public partial class ClientRegistrationDataSet
    {
        partial class ClientDataTable
        {
            protected override void OnTableNewRow(System.Data.DataTableNewRowEventArgs e)
            {
                e.Row[this.ClientIdColumn] = System.Guid.NewGuid();
                base.OnTableNewRow(e);
            }
        }
    }
}
