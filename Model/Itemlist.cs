using System;
using ToDoApp_v1._2.Abstract;

namespace ToDoApp_v1._2.Model
{
    public class Itemlist : BaseInfo //  ---> Inheritance
    {
        public int ItemlistId { get; set; }
        public string Detailed { get; set; }
        public string Status { get; set; }

        
        public virtual Datalist Datalist { get; set; }
    }
}
