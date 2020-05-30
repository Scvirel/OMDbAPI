using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMDbAPI.Components
{
    internal class FilmList
    {
        public List<Film> List { get { return filmList; } set { filmList = value; } }

        private List<Film> filmList;

        public FilmList()
        {
            GenereteFilmList();
        }

        public List<Film> GenereteFilmList()
        {
            if (filmList != null) return filmList;

            filmList = new List<Film>();
            return filmList;
        }

        public void AddFilm(Film film)
        {
            if (filmList == null) throw new NullReferenceException();

            filmList.Add(film);
        }

        public void RemoveFilm(Film film)
        {
            if (filmList == null) throw new NullReferenceException();

            filmList.Remove(film);
        }

        public void ClearFilmList()
        {
            if (filmList == null) throw new NullReferenceException();

            filmList.Clear();
        }
    }
}
