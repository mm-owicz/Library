using System.Text.Json;

namespace Lab1.Models;

public class JSONModel {
   public static List<User>? ReadFromJSONUsers() {
        List<User> users = new List<User>();

        // read the data
        using (StreamReader r = new StreamReader("users.json")) {
        string json = r.ReadToEnd();
        users = JsonSerializer.Deserialize<List<User>>(json);
        }

        return users;
    }
    public static void WriteToJSONUsers(List<User>? users){
        // save the data
        string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true });
        using (StreamWriter outputFile = new StreamWriter("users.json")) {
        outputFile.WriteLine(jsonString);
        }
    }

    public static List<Book>? ReadFromJSONBooks() {
        List<Book> books = new List<Book>();

        // read the data
        using (StreamReader r = new StreamReader("books.json")) {
        string json = r.ReadToEnd();
        books = JsonSerializer.Deserialize<List<Book>>(json);
        }

        return books;
    }
    public static void WriteToJSONBooks(List<Book>? books){
        // save the data
        string jsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions() { WriteIndented = true });
        using (StreamWriter outputFile = new StreamWriter("books.json")) {
        outputFile.WriteLine(jsonString);
        }
    }
}