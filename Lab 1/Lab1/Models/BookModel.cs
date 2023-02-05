namespace Lab1.Models;

public class Book {
    public int id { get; set; }
    public string? author { get; set; }
    public string? title { get; set; }
    public int date{ get; set; }
    public string? publisher { get; set; }
    public string? user { get; set; }
    public string? reserved { get; set; }
    public string? leased { get; set; }

    public bool isReserved(){
        if (reserved == ""){return false;}
        DateTime res = Convert.ToDateTime(reserved);
        DateTime n = DateTime.Now;
        bool x = (res > DateTime.Now) ? true : false;
        if (!x){reserved = "";}
        return x;
    }

    public bool isLeased(){
        if (leased == ""){return false;}
        else {return true;}
    }
}