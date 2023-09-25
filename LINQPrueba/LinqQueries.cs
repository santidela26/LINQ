using System.Collections;
using System.Text.Json;

namespace LINQPrueba;

public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();

    public LinqQueries()
    {
        using (StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            librosCollection = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public IEnumerable<Book> TodaLaColeccion()
    {
        return librosCollection;
    }

    public IEnumerable<Book> LibrosDespuesDel2000()
    {
        //Extension Method
        return librosCollection.Where(p => p.PublishedDate.Year >= 2000).ToList();

        //Query Expression
        //return from l in librosCollection where l.PublishedDate.Year >= 2000 select l;
    }

    public IEnumerable<Book> LibrosMas250paginasYContengaPalabraAction()
    {
        //Extension Method
        return librosCollection.Where(p => p.PageCount > 250 && p.Title.Contains("in Action"));

        //Query Expression
        //return from l in librosCollection where l.PageCount > 250 && l.Title.Contains("in Action") select l;
    }

    public bool TodosLosLibrosTienenStatus()
    {
        //All verifica que todos los elementos de la coleccion cumplan con la condición.
        //En este caso, verifica si todos los libros no estan vacios.
        //Como no está vacio retorna true, de lo contrario retornaria false
        return librosCollection.All(p => p.Status != string.Empty);
    }

    public bool AlgunLibroPublicadoEn2005()
    {
        //Any erifica que al menos un elemento de la coleccion cumpla con la condicion.
        //En este caso, verifica si al menos un libro fue publicado en 2005
        //Como hay libros que si fueron publicados en 2005 retorna true, de lo contrario retornaria false
        return librosCollection.Any(p => p.PublishedDate.Year == 2005);
    }

    public IEnumerable<Book> LibrosDePython()
    {
        //Contains obtiene los libros que en su categoria contengan la palabra "Python"
        return librosCollection.Where(p => p.Categories.Contains("Python")).ToList();
    }

    public IEnumerable<Book> OrderByLibrosQueContengaJavaEnCategoria()
    {
        //OrderBy va a ordenar los libros de forma ascendente en base a sus titulos que contengan la categoria "Java" 
        return librosCollection.Where(p => p.Categories.Contains("Java")).OrderBy(p => p.Title);
    }

    public IEnumerable<Book> OrderByDescendingQueContenganMasDe450Paginas()
    {
        //OrderByDescending va a ordenar los libros de forma descendente en base a su cantidad de paginas que contengan más de 450 páginas 
        return librosCollection.Where(p => p.PageCount > 450).OrderByDescending(p => p.PageCount);
    }

    public IEnumerable<Book> TresPrimeroLibrosOrdenadosPorFecha()
    {
        //Take obtiene los primeros elementos
        //TakeLast obtiene los ultimos elementos
        //TakeWhile recorre la coleccion hasta obtener los elementos que cumplan la condición

        return librosCollection.Where(p => p.Categories.Contains("Java")).OrderByDescending(p => p.PublishedDate)
            .Take(3);

        /* return librosCollection.Where(p => p.Categories.Contains("Java")).OrderByDescending(p => p.PublishedDate)
             .TakeLast(3);*/

    }

    public IEnumerable<Book> TercerYCuartoLibroConMasDe400Paginas()
    {
        //Obtengo 4 libros con Take pero salteo los 2 primeros con Skip

        return librosCollection.Where(p => p.PageCount > 400).Take(4).Skip(2);
    }

    public IEnumerable<Book> ImprimirTituloYPaginaDeLosPrimerosTresLibrosDeLaLista()
    {
        //Selecciono solamente los campos Title y PageCount utilizando Select

        return librosCollection
            .Take(3)
            .Select(p => new Book { Title = p.Title, PageCount = p.PageCount });
    }

    public int LibrosEntre200Y500Paginas()
    {
        //Count cuenta la cantidad de libros (int) que cumplen la condicion
        //LongCount hace lo mismo que Count pero es de tipo "long"

        return librosCollection.Count(p => p.PageCount >= 200 && p.PageCount <= 500);
    }

    public DateTime FechaDePublicacionMenor()
    {
        //MIN y MAX son buenos para tipos primitivos, una lista de string, int, etc
        //Con MIN retorna el valor, en este caso, la fecha menor de todos los libros que hay

        return librosCollection.Min(p => p.PublishedDate);
    }

    public int MayorCantidadDePaginasLibro()
    {
        //MIN y MAX son buenos para tipos primitivos, una lista de string, int, etc
        //Con Max retorna el valor, en este caso, el mayor de número de páginas que hay en un libros
        //dentro de todos los libros que hay

        return librosCollection.Max(p => p.PageCount);
    }

    public Book LibroConMenorNumeroDePaginas()
    {
        //MinBy y MaxBy retorna el objeto completo
        //En este caso el libro tenia que ser mayor a 0 ya que hay libros con 0 páginas
        return librosCollection.Where(p => p.PageCount > 0).MinBy(p => p.PageCount);
    }

    public Book LibronConFechaMasReciente()
    {
        //MinBy y MaxBy retorna el objeto completo
        return librosCollection.MaxBy(p => p.PublishedDate);
    }

    public int SumaDeTodasLasPaginasLibrosEntre0y500()
    {
        //Sum lo que hace es sumar el valor que se envie
        //En este caso, obtienen los libros que contengan entre 0 y 500 páginas para luego sumarlas y devolver un total de páginas
        //sumando todos los libros que cumplan la condicion
        return librosCollection.Where(p => p.PageCount >= 0 && p.PageCount <= 500).Sum(p => p.PageCount);
    }

     public string TitulosDeLibrosDespuesDel2015Concatenados()
     {

        //Aggregate se usa para realizar la acumulación de algún dato dentro de una variable y retornar este valor acumulado
        //Sintaxis de Aggregate: Aggregate( valorInicialDelAcumulador, (Acumulador, Elemento), Funcion ); 
        //Las comillas "" es el valor inicial que va a tener. Si quiero que sea con enteros, coloco un 0, etc
        //TitulosLibros es la variable donde se va acumular
        //next es la variable que va a tener los siguientes elementos de la coleccion, se llama next por estandar
        //En la función voy agregar la lógica que quiera

        return librosCollection.Where(p => p.PublishedDate.Year > 2015).
            Aggregate("", (TitulosLibros, next) =>
            {
                if(TitulosLibros != string.Empty)
                {
                    TitulosLibros = TitulosLibros + " - " + next.Title;
                }
                else
                {
                    TitulosLibros = TitulosLibros + next.Title;
                }

                return TitulosLibros;
            });

    }

    public int MayorCantidadDePaginas()
    {
        return librosCollection.Aggregate(0, (CantidadPaginas, next) =>
        {
            if (CantidadPaginas < next.PageCount)
            {
                CantidadPaginas = next.PageCount;
            }

            return CantidadPaginas;
        });
    }


    public double PromedioDeCaracteresDeCadaTitulo()
    {
        //Average permite sacar promedios de un valor númerico, devuelve double

        return librosCollection.Average(p => p.Title.Length);
    }

    public double PromedioDeCantidadDePaginas()
    {
        return librosCollection.Where(p => p.PageCount > 0).Average(p => p.PageCount);
    }

    public IEnumerable<IGrouping<int, Book>> LibrosDespuesDel2000AgrupadosPorAño()
    {
        //GroupBy permite agrupar los datos por una propiedad

        /*IGrouping<TKey, TElement> se utiliza cuando deseas obtener tanto la clave como los elementos de un grupo.
        * Para acceder a los elementos, primero obtienes la clave y luego iteras a través de los elementos del grupo.*/

        return librosCollection.Where(p => p.PublishedDate.Year > 2000).GroupBy(p => p.PublishedDate.Year);
    }

    public ILookup<char, Book> DiccionarioDeLibrosPorLetra()
    {
        //ToLookUp agrupa datos, en este caso los va agrupar en base a la primera letra del título

        /*ILookup<TKey, TElement> se utiliza cuando deseas acceder directamente a los elementos de un grupo sin necesidad de obtener la clave primero.
        * Puedes buscar y acceder a los elementos por clave de manera eficiente.*/

        return librosCollection.ToLookup(p => p.Title[0], p => p);
    }

    public IEnumerable<Book> LibrosConMasDe500PagYLibrosPublicadosDespuesDel2005()
    {
        //Join se utiliza para unir colecciones 

        /*Sintaxis de Join: primeraColeccion
        *.Join(segundaColeccion, valorDePrimeraColeccionQueCoincidaConLaSegundaColeccion, valorDeLaSegundaColeccionQueConCoincidaConLaPrimeraColeccion, valor de la coleccion que queramos retornar)*/

        IEnumerable<Book> librosConMasDe500Paginas = librosCollection.Where(p => p.PageCount > 500);
        IEnumerable<Book> librosPublicadosDespuesDel2005 = librosCollection.Where(p => p.PublishedDate.Year > 2005);


        //Lo que va a devolver son libros que sean de más de 500 pág y publicados después del 2005
        return librosConMasDe500Paginas.Join(librosPublicadosDespuesDel2005, p => p.Title, x => x.Title, (p, x) => p);
    }
}