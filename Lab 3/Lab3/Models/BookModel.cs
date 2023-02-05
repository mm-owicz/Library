using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.Models;
public class Book {
    public int Bookid {get; set;}
    public string? author { get; set; }
    public string? title { get; set; }
    public int date{ get; set; }
    public string? publisher { get; set; }
    public string? user { get; set; }
    public DateTime? reserved { get; set; }
    public DateTime? leased { get; set; }

    [Timestamp]
    public byte[]? RowVersion {get; set;}


    public bool isReserved(){
        if (reserved == null){return false;}
        DateTime res = Convert.ToDateTime(reserved);
        DateTime n = DateTime.Now;
        bool x = (res > DateTime.Now) ? true : false;
        if (!x){reserved = null;}
        return x;
    }

    public bool isLeased(){
        if (leased == null){return false;}
        else {return true;}
    }

}