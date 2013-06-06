namespace CN100.EnterprisePlatform.ORM.DB
{
    public class OrlOrder
    {
        private string column;
        private bool asc = true;

        public OrlOrder(string column, bool asc)
        {
            this.column = column.ToLower();
            this.asc = asc;
        }

        public string Column
        {
            get { return column; }
        }

        public bool Ascending
        {
            get { return asc; }
        }

        public string GetSql(OrlTable table)
        {
            string name = table.Translate(column);
            string dir = asc ? "asc" : "desc";
            return string.Format("[{0}] {1}", name, dir);
        }
    }
}
