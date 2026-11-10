namespace CORE.Domain
{
    public abstract class Record
    {
        //private int id; // field

        //public void setId(int id) // behavior
        //{
        //    this.id = id;
        //}

        //public int getId()
        //{
        //    return id;
        //}

        public int Id { get; set; } // property

        protected Record()
        {
        }

        protected Record(int id)
        {
            Id = id;
        }
    }
}
