async function GetAll() {
    const bookResponse = await fetch("/books", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const authorResponse = await fetch("/authors", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const genreResponse = await fetch("/genres", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (bookResponse.ok === true) {
        const books = await bookResponse.json();

        let bookRows = document.querySelector("div.books");

        books.forEach(book => {
            bookRows.append(card(book));
        });
    }

    if (authorResponse.ok === true) {
        const authors = await authorResponse.json();

        authors.forEach(author => {
            ulauthor(author);
        });
    }

    if (genreResponse.ok === true) {
        const genres = await genreResponse.json();

        genres.forEach(genre => {
            ulgenres(genre);
        });
    }
}

async function CreateAuthor(name, surname) {

    const authorResponse = await fetch("/authors", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: name,
            surname: surname
        })
    });
    if (authorResponse.ok === true) {
        reset();
    }
    window.location.reload();
}

async function CreateGenre(name) {

    const response = await fetch("/genres", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: name
        })
    });
    if (response.ok === true) {
        reset();
    }
    window.location.reload();
}

async function CreateBook(name, summary, author, genre) {

    const response = await fetch("/books", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: name,
            summary: summary,
            authorId: author,
            genreId: genre
        })
    });
    if (response.ok === true) {
        reset();
    }
    window.location.reload();
}

async function EditGenre(genreId, name) {
    const response = await fetch("genres", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(genreId, 10),
            name: name
        })
    });
    if (response.ok === true) {
        reset();
        window.location.reload();
    }
}

async function DeleteBook(id) {
    const response = await fetch("/books/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        window.location.reload();
    }
}

async function DeleteAuthor(id) {
    const response = await fetch("/authors/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        window.location.reload();
    }
}

async function DeleteGenre(id) {
    const response = await fetch("/genres/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        window.location.reload();
    }
}

function reset() {
    const form1 = document.forms["addBookForm"];
    const form2 = document.forms["addAuthorForm"];
    const form3 = document.forms["addGenreForm"];
    form1.reset();
    form2.reset();
    form3.reset();
}

function card(book) {

    const div = document.createElement('div');
    div.className = 'card';

    const img = document.createElement('img');
    img.src = 'image/noimagefound.jpg';
    img.className = 'card-img-top';
    div.append(img);

    const divB = document.createElement('div');
    divB.className = 'card-body';
    div.append(divB);

    const h5 = document.createElement('h5');
    h5.className = 'card-title';
    h5.textContent = book.name;
    divB.append(h5);

    const p1 = document.createElement('p');
    p1.className = 'card-text';
    p1.textContent = 'Краткое содержание: ' + book.summary;
    divB.append(p1);

    const p2 = document.createElement('p');
    p2.className = 'card-text';
    p2.textContent = 'Автор: ' + book.author;
    divB.append(p2);

    const p3 = document.createElement('p');
    p3.className = 'card-text';
    p3.textContent = 'Жанр: ' + book.genre;
    divB.append(p3);

    const a1 = document.createElement('a');
    a1.className = 'card-link';
    a1.innerHTML = '<img src="image/download.png" style="cursor:pointer; width: 25px; height: 25px;"/>';
    divB.append(a1);

    const a2 = document.createElement('a');
    a2.className = 'card-link';
    a2.innerHTML = '<img src="image/edit.png" style="cursor:pointer; width: 20px; height: 20px;"/>';
    divB.append(a2);

    const a3 = document.createElement('a');
    a3.setAttribute("data-id", book.id);
    a3.setAttribute("style", "cursor:pointer;");
    a3.className = 'card-link';
    a3.innerHTML = '<img src="image/delete.png" style="cursor:pointer; width: 20px; height: 20px;"/>';
    divB.append(a3);
    a3.addEventListener("click", e => {
        e.preventDefault();
        DeleteBook(book.id);
    });

    return div;
}

function ulauthor(author) {
    var ul = document.getElementById('authors');
    var li = document.createElement('li');
    li.className = 'detail list-group-item';
    li.innerHTML = author.name + ' ' + author.surname + '<img src="image/edit.png" style="cursor:pointer; width: 20px; height: 20px; margin: 10px;"/><img src="image/delete.png" data-id="' + author.authorId + '" style="cursor:pointer; width: 20px; height: 20px;"/>';
    ul.appendChild(li);
    li.addEventListener("click", e => {
        e.preventDefault();
        DeleteAuthor(author.authorId);
    });

    var sel = document.getElementById('authorSelector');
    var opt = document.createElement('option');
    opt.value = author.authorId;
    opt.appendChild(document.createTextNode(author.name + ' ' + author.surname));
    sel.appendChild(opt);
}

function ulgenres(genre) {
    var ul = document.getElementById('genres');
    var li = document.createElement('li');
    li.className = 'detail list-group-item';
    li.innerHTML = genre.name + '<img src="image/edit.png" style="cursor:pointer; width: 20px; height: 20px; margin: 10px;"/><img src="image/delete.png" id="del-gen-' + genre.genreId + '" data-id="' + genre.genreId + '" style="cursor:pointer; width: 20px; height: 20px;"/>';
    ul.appendChild(li);
    document.getElementById('del-gen-' + genre.genreId).addEventListener("click", e => {
        e.preventDefault();
        DeleteGenre(genre.genreId);
    });

    var sel = document.getElementById('genreSelector');
    var opt = document.createElement('option');
    opt.value = genre.genreId;
    opt.appendChild(document.createTextNode(genre.name));
    sel.appendChild(opt);
}

document.forms["addBookForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["addBookForm"];
    const name = form.elements["name"].value;
    const summary = form.elements["summary"].value;
    const author = form.elements["author"].value;
    const genre = form.elements["genre"].value;

    CreateBook(name, summary, author, genre);
});

document.forms["addAuthorForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["addAuthorForm"];
    const name = form.elements["name"].value;
    const surname = form.elements["surname"].value;

    CreateAuthor(name, surname);
});

document.forms["addGenreForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["addGenreForm"];
    const name = form.elements["name"].value;

    CreateGenre(name);
});

GetAll();