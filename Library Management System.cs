using System;
// abstract class for representing a person
public abstract class Person{
    public string Name;
    public int Id;
    
    // constructor
    public Person(string name, int id){
        Name = name;
        Id = id;
    }

    // constructor overloading
    public Person(string name){
        Name = name;
    }

    // abstract method
    public abstract void Show();

}

public class Book{
    public string Title;
    public int BookId;
    public bool Issued;

    // constructor 
    public Book(string title, int bookId, bool issued = false){
        Title = title;
        BookId = bookId;
        Issued = issued;
    }

    // copy constructor
    public Book(Book other){
        Title = other.Title;
        BookId = other.BookId;
        Issued = other.Issued;
    }

    // method to issue a book
    public void Issue(){
        Issued = true;
    }

    // method to return a book
    public void Return(){
        Issued = false;
    }

    // book status
    public string Status(){
        if(Issued){
            return $"Book ID : {BookId}, Title : {Title}, Status : Unavailable";
        }
        else{
            return $"Book ID : {BookId}, Title : {Title}, Status : Available";
        }
    }

}

// inheritance from Person
public class Member : Person{
    public string MembershipType;
    public Member(string name, int id, string membershipType) : base(name, id){
        MembershipType = membershipType;
    }

    // overriding Show
    public override void Show(){
        Console.WriteLine($"Member Id : {Id}, Name : {Name}, Membership Type : {MembershipType}");
    }

    // adding a member
    public Member[] members = new Member[100];
    public int MemberBookCount = 0;
    public void AddMember(Member member){
        members[MemberBookCount++] = member;
        Console.WriteLine("Member registered successfully");
    }

    // operator overloading 
    public static int operator+ (Member m1, Member m2){
        return m1.MemberBookCount + m2.MemberBookCount;
    }

}

// interface
public interface GenerateReport{
    void Report();
}

public class Transaction : GenerateReport{
    public int MemberId;
    public int BookId;
    public int DueDate;

    public Transaction(int memberId, int bookId, int dueDate){
        MemberId = memberId;
        BookId = bookId;
        DueDate = dueDate;
    }
    
    // show transaction
    public void DisplayTransaction(){
        Console.WriteLine($"Member Id : {MemberId}, Book Id : {BookId}, Due Date : {DueDate}");
    }

    public Transaction[] transactions = new Transaction[100];
    public int Transcount = 0;

    // adding transaction
    public void AddTransaction(Transaction transaction){
        transactions[Transcount++] = transaction;
    }

    // to generate report 
    public void Report(){
        Console.WriteLine("Issued Books Report : ");
        for(int i = 0; i < Transcount; i++){
            transactions[i].DisplayTransaction();
        }
    }

}

// static class 
public static class BookOperation{
    public static Book[] books = new Book[100];
    public static int BookCount = 0; // static variable
    // static method
    public static void IssueBook(int bookId){
        for(int i = 0; i < BookCount; i++){
            if(books[i].BookId == bookId){
                books[i].Issue();
                Console.WriteLine("Book Issued Successfully");
                return;
            }
        }
        Console.WriteLine("Book not available!");
    }

    public static void ReturnBook(int bookId){
        for(int i = 0; i < BookCount; i++){
            if(books[i].BookId == bookId){
                books[i].Return();
                Console.WriteLine("Book Returned Successfully");
                return;
            }
        }
        Console.WriteLine("Book not found");
    }
    
}

class Library{
    public static void Main(string[] args){

        // adding book to library
        Book book1 = new Book("Sherlock Holmes", 1, false);
        Book book2 = new Book("Rich Dad, Poor Dad", 2, false);

        // populating bookoperation class
        BookOperation.books[BookOperation.BookCount++] = book1;
        BookOperation.books[BookOperation.BookCount++] = book2;

        // book status
        Console.WriteLine(book1.Status());
        Console.WriteLine(book2.Status());

        // registering members
        Member member1 = new Member("Rifat", 101, "Platinum");
        Member member2 = new Member("Safayet", 102, "Gold");

        member1.Show();
        member2.Show();

        // operator overloading by summing up book counts
        member1.AddMember(member1);
        member2.AddMember(member2);
        int total = member1 + member2;
        Console.WriteLine($"Total books issued by members : {total}");

        // issuing books
        Console.WriteLine("\nIssuing Book 1...");
        BookOperation.IssueBook(1);
        
        // returning books
        Console.WriteLine("\nReturning Book 1...");
        BookOperation.ReturnBook(1);

        // adding a transaction
        Transaction t1 = new Transaction(101, 1, 7);
        t1.DisplayTransaction();

        // generating report using interface
        t1.AddTransaction(t1);
        t1.Report();

        Console.WriteLine("\nLibrary System Check Completed\n");

    }
}