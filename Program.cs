using System;

class Program
{
    static void Main()
    {
        // Each slot has a title + checked-out flag
        string book1 = "", book2 = "", book3 = "", book4 = "", book5 = "";
        bool out1 = false, out2 = false, out3 = false, out4 = false, out5 = false;

        const int borrowLimit = 3;

        while (true)
        {
            Console.WriteLine("\nChoose an action: add, remove, search, checkout, checkin, display, exit");
            Console.Write("Action: ");
            string action = (Console.ReadLine() ?? "").ToLower();

            if (action == "add")
            {
                Console.Write("Enter the title of the book to add: ");
                string title = Console.ReadLine() ?? "";

                if (IsEmpty(book1)) book1 = title;
                else if (IsEmpty(book2)) book2 = title;
                else if (IsEmpty(book3)) book3 = title;
                else if (IsEmpty(book4)) book4 = title;
                else if (IsEmpty(book5)) book5 = title;
                else Console.WriteLine("The library is full. No more books can be added.");
            }
            else if (action == "remove")
            {
                if (AllEmpty(book1, book2, book3, book4, book5))
                {
                    Console.WriteLine("Library is empty. No books to remove.");
                    continue;
                }

                Console.Write("Enter the title of the book to remove: ");
                string title = Console.ReadLine() ?? "";

                bool removed = false;

                if (Matches(book1, title)) { book1 = ""; out1 = false; removed = true; }
                else if (Matches(book2, title)) { book2 = ""; out2 = false; removed = true; }
                else if (Matches(book3, title)) { book3 = ""; out3 = false; removed = true; }
                else if (Matches(book4, title)) { book4 = ""; out4 = false; removed = true; }
                else if (Matches(book5, title)) { book5 = ""; out5 = false; removed = true; }

                Console.WriteLine(removed ? "Book removed successfully." : "Book not found.");
            }
            else if (action == "search")
            {
                Console.Write("Enter the title of the book to search: ");
                string title = Console.ReadLine() ?? "";

                int slot = FindSlot(book1, book2, book3, book4, book5, title);

                if (slot == 0)
                {
                    Console.WriteLine("The book is not in the collection.");
                }
                else
                {
                    bool isOut = GetCheckedOut(slot, out1, out2, out3, out4, out5);
                    Console.WriteLine(isOut
                        ? "The book is in the library but it is currently checked out."
                        : "The book is available in the library.");
                }
            }
            else if (action == "checkout")
            {
                int borrowedCount = CountCheckedOut(out1, out2, out3, out4, out5);

                if (borrowedCount >= borrowLimit)
                {
                    Console.WriteLine("Borrowing limit reached. You can only check out 3 books at a time.");
                    continue;
                }

                Console.Write("Enter the title of the book to check out: ");
                string title = Console.ReadLine() ?? "";

                int slot = FindSlot(book1, book2, book3, book4, book5, title);

                if (slot == 0)
                {
                    Console.WriteLine("Book not found in the library.");
                }
                else
                {
                    bool isOut = GetCheckedOut(slot, out1, out2, out3, out4, out5);

                    if (isOut)
                    {
                        Console.WriteLine("That book is already checked out.");
                    }
                    else
                    {
                        SetCheckedOut(slot, true, ref out1, ref out2, ref out3, ref out4, ref out5);
                        Console.WriteLine("Book checked out successfully.");
                        Console.WriteLine("Books currently checked out: " + CountCheckedOut(out1, out2, out3, out4, out5));
                    }
                }
            }
            else if (action == "checkin")
            {
                Console.Write("Enter the title of the book to check in: ");
                string title = Console.ReadLine() ?? "";

                int slot = FindSlot(book1, book2, book3, book4, book5, title);

                if (slot == 0)
                {
                    Console.WriteLine("Book not found in the library.");
                }
                else
                {
                    bool isOut = GetCheckedOut(slot, out1, out2, out3, out4, out5);

                    // Requirement: if checked out -> remove flag; else inform user
                    if (isOut)
                    {
                        SetCheckedOut(slot, false, ref out1, ref out2, ref out3, ref out4, ref out5);
                        Console.WriteLine("Book checked in successfully.");
                        Console.WriteLine("Books currently checked out: " + CountCheckedOut(out1, out2, out3, out4, out5));
                    }
                    else
                    {
                        Console.WriteLine("That book is not checked out, so it cannot be checked in.");
                    }
                }
            }
            else if (action == "display")
            {
                DisplayBooks(book1, out1);
                DisplayBooks(book2, out2);
                DisplayBooks(book3, out3);
                DisplayBooks(book4, out4);
                DisplayBooks(book5, out5);

                if (AllEmpty(book1, book2, book3, book4, book5))
                    Console.WriteLine("No books in the library.");
            }
            else if (action == "exit")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid action. Please type add, remove, search, checkout, checkin, display, or exit.");
            }
        }
    }

    static bool IsEmpty(string s) => string.IsNullOrWhiteSpace(s);

    static bool AllEmpty(string a, string b, string c, string d, string e)
        => IsEmpty(a) && IsEmpty(b) && IsEmpty(c) && IsEmpty(d) && IsEmpty(e);

    static bool Matches(string storedTitle, string inputTitle)
        => !IsEmpty(storedTitle) && storedTitle.Equals(inputTitle, StringComparison.OrdinalIgnoreCase);

    static int FindSlot(string b1, string b2, string b3, string b4, string b5, string title)
    {
        if (Matches(b1, title)) return 1;
        if (Matches(b2, title)) return 2;
        if (Matches(b3, title)) return 3;
        if (Matches(b4, title)) return 4;
        if (Matches(b5, title)) return 5;
        return 0;
    }

    static int CountCheckedOut(bool o1, bool o2, bool o3, bool o4, bool o5)
    {
        int count = 0;
        if (o1) count++;
        if (o2) count++;
        if (o3) count++;
        if (o4) count++;
        if (o5) count++;
        return count;
    }

    static bool GetCheckedOut(int slot, bool o1, bool o2, bool o3, bool o4, bool o5)
    {
        return slot switch
        {
            1 => o1,
            2 => o2,
            3 => o3,
            4 => o4,
            5 => o5,
            _ => false
        };
    }

    static void SetCheckedOut(int slot, bool value, ref bool o1, ref bool o2, ref bool o3, ref bool o4, ref bool o5)
    {
        switch (slot)
        {
            case 1: o1 = value; break;
            case 2: o2 = value; break;
            case 3: o3 = value; break;
            case 4: o4 = value; break;
            case 5: o5 = value; break;
        }
    }

    static void DisplayBooks(string title, bool isCheckedOut)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine(isCheckedOut ? $"- {title} (Checked Out)" : $"- {title} (Available)");
        }
    }
}
