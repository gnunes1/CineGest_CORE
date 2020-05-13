namespace CineGest.Models
{
    /// <summary>
    /// Relaciona N cinema a N filmes
    /// </summary>
    public class Cinema_Movie
    {
        //PrimaryKey(MovieFK, CinemaFK)

        /// <summary>
        /// faz referência ao filme e faz chave composta com CinemaFK
        /// </summary>
        public int MovieFK { get; set; }

        /// <summary>
        /// faz referência ao cinema e faz chave composta com MovieFK
        /// </summary>
        public int CinemaFK { get; set; }

        /// <summary>
        /// chave estrangeira que representa o filme
        /// </summary>
        public Movies Movie { get; set; }

        /// <summary>
        /// chave estrangeira que representa o cinema
        /// </summary>
        public Cinemas Cinema { get; set; }

    }

}
