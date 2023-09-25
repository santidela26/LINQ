using LINQPrueba;

LinqQueries queries = new LinqQueries();

//ImprimirValores(queries.TodaLaColeccion());

//ImprimirValores(queries.LibrosDespuesDel2000());

//ImprimirValores(queries.LibrosMas250paginasYContengaPalabraAction());

//Console.WriteLine($"Todos los libros tienen status? - {queries.TodosLosLibrosTienenStatus()}");

//Console.WriteLine($"Algún libro fue publicado en 2005? - {queries.AlgunLibroPublicadoEn2005()}");

//ImprimirValores(queries.LibrosDePython());

//ImprimirValores(queries.OrderByLibrosQueContengaJavaEnCategoria());

//ImprimirValores(queries.OrderByDescendingQueContenganMasDe450Paginas());

//ImprimirValores(queries.TresPrimeroLibrosOrdenadosPorFecha());

//ImprimirValores(queries.TercerYCuartoLibroConMasDe400Paginas());

//ImprimirValores(queries.ImprimirTituloYPaginaDeLosPrimerosTresLibrosDeLaLista());

//Console.WriteLine($"Cantidad de libros entre 200 y 500 páginas - {queries.LibrosEntre200Y500Paginas()}");

//Console.WriteLine($"Fecha de libro más viejo - {queries.FechaDePublicacionMenor()}");

//Console.WriteLine($"Cantidad de páginas del libro que contiene más páginas - {queries.MayorCantidadDePaginasLibro()}");

/*
Book libroMenorPag = queries.LibroConMenorNumeroDePaginas();
Console.WriteLine($"El libro con menor núm de pág es {libroMenorPag.Title} con una cantidad de {libroMenorPag.PageCount}");
*/

/*
Book libroMasReciente = queries.LibronConFechaMasReciente();
Console.WriteLine($"El libro más reciente es {libroMasReciente.Title} publicado en {libroMasReciente.PublishedDate}");
*/

//Console.WriteLine($"Suma total de páginas de libros que tengan entre 0 y 500 pág: {queries.SumaDeTodasLasPaginasLibrosEntre0y500()}");

//Console.WriteLine(queries.TitulosDeLibrosDespuesDel2015Concatenados());

//Console.WriteLine(queries.MayorCantidadDePaginas());

//Console.WriteLine(queries.PromedioDeCaracteresDeCadaTitulo());

//Console.WriteLine(queries.PromedioDeCantidadDePaginas());

//ImprimirGrupo(queries.LibrosDespuesDel2000AgrupadosPorAño());

/*var diccionarioLookup = queries.DiccionarioDeLibrosPorLetra();

ImprimirDiccionario(diccionarioLookup, 'S');*/

//ImprimirValores(queries.LibrosConMasDe500PagYLibrosPublicadosDespuesDel2005());

void ImprimirValores(IEnumerable<Book> listaDeLibros)
{
    Console.WriteLine("{0, -58} {1, 15} {2, 15}", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach (var item in listaDeLibros)
    {
        Console.WriteLine("{0, -58} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}

void ImprimirGrupo(IEnumerable<IGrouping<int, Book>> ListadeLibros)
{
    /*IGrouping<TKey, TElement> se utiliza cuando deseas obtener tanto la clave como los elementos de un grupo.
    * Para acceder a los elementos, primero obtienes la clave y luego iteras a través de los elementos del grupo.*/

    foreach (var grupo in ListadeLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: {grupo.Key}");
        Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach (var item in grupo)
        {
            Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
        }
    }
}

void ImprimirDiccionario(ILookup<char, Book> bookList, char letter)
{
    /*ILookup<TKey, TElement> se utiliza cuando deseas acceder directamente a los elementos de un grupo sin necesidad de obtener la clave primero.
     * Puedes buscar y acceder a los elementos por clave de manera eficiente.*/

    Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach (var item in bookList[letter])
    {
        Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
    }

}