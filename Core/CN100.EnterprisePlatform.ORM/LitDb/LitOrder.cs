namespace CN100.EnterprisePlatform.ORM.DB
{
    public class LitOrder
    {
        private string column;
        private bool asc = true;

        public LitOrder(string column, bool asc)
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

        public string GetSql(LitTable table)
        {
            string name = table.Translate(column);
            string dir = asc ? "asc" : "desc";
            return string.Format("[{0}] {1}", name, dir);
        }
    }
}
