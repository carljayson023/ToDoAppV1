using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp_v1._2.Abstract;

namespace ToDoApp_v1._2.Model
{
    public class Datalist : BaseInfo //  ---> Inheritance
    {
       
        public string Description { get; set; }

        public virtual ICollection<Itemlist> Itemlists{ get; private set; } = new ObservableCollection<Itemlist>();
    }
}
