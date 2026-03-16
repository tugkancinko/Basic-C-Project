📚 Simple Library Management System (C#)
Overview

This project is a simple console-based Library Management System written in C#.
It allows users to manage a small collection of books through a command-line interface.

The program supports basic library operations such as:

Adding books

Removing books

Searching for books

Checking out books

Checking in books

Displaying the current library collection

The system stores up to 5 books and enforces a maximum borrowing limit of 3 books at a time.

🚀 Features
Add Books

Users can add a new book to the library if there is available space.

Remove Books

Books can be removed from the collection by entering the title.

Search Books

Search for a specific book to check:

If it exists in the library

If it is currently checked out

Checkout Books

Users can borrow books with the following rules:

A maximum of 3 books can be borrowed at the same time

Books already checked out cannot be borrowed again

Checkin Books

Return previously borrowed books to the library.

Display Library

Shows the current list of books and their status:

Available

Checked Out

🧠 Program Logic

The program maintains:

5 book slots (maximum capacity)

Each book has:

A title

A checked-out status (true/false)
