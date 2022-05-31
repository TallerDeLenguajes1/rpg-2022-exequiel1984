using rpg_2022_exequiel1984;

namespace rpg_2022_exequiel1984
{
    public class personaje {
        private Caracteristicas caracteristicas; 
        private Datos datos;

        public Caracteristicas Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
        public Datos Datos { get => datos; set => datos = value; }

        public personaje(){
            this.Caracteristicas = new Caracteristicas();

            this.Datos = new Datos();
            

        }
    }
}