using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Patrones_Estructurales.Proxy.Ejercicio1_ControlAcceso
{
    // Interfaz común
    public interface IDocumento
    {
        string Titulo { get; }
        string Contenido { get; }
        string Autor { get; }
        void Mostrar();
    }

    // Sujeto Real - Documento confidencial
    public class DocumentoConfidencial : IDocumento, INotifyPropertyChanged
    {
        private string _titulo;
        private string _contenido;
        private string _autor;

        public event PropertyChangedEventHandler PropertyChanged;

        public DocumentoConfidencial(string titulo, string contenido, string autor)
        {
            _titulo = titulo;
            _contenido = contenido;
            _autor = autor;
        }

        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }

        public string Contenido
        {
            get => _contenido;
            set
            {
                _contenido = value;
                OnPropertyChanged();
            }
        }

        public string Autor
        {
            get => _autor;
            set
            {
                _autor = value;
                OnPropertyChanged();
            }
        }

        public void Mostrar() { }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // Proxy con INotifyPropertyChanged
    public class ProxySeguridad : IDocumento, INotifyPropertyChanged
    {
        private DocumentoConfidencial _documentoReal;
        private string _usuarioActual;
        private Dictionary<string, List<string>> _permisos;
        private string _rolActual;
        private List<string> _permisosLista;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProxySeguridad(string titulo, string contenido, string autor)
        {
            _permisos = new Dictionary<string, List<string>>
            {
                { "Admin", new List<string> { "Ver", "Editar", "Eliminar", "Crear" } },
                { "Editor", new List<string> { "Ver", "Editar" } },
                { "Lector", new List<string> { "Ver" } },
                { "Invitado", new List<string>() }
            };

            _documentoReal = new DocumentoConfidencial(titulo, contenido, autor);
            _usuarioActual = "invitado";
            ActualizarPermisos();
        }

        public string UsuarioActual
        {
            get => _usuarioActual;
            set
            {
                _usuarioActual = value;
                ActualizarPermisos();
                OnPropertyChanged(nameof(UsuarioActual));
                OnPropertyChanged(nameof(RolActual));
                OnPropertyChanged(nameof(PermisosTexto));
                OnPropertyChanged(nameof(TieneAcceso));
                OnPropertyChanged(nameof(PuedeEditar));
                OnPropertyChanged(nameof(PuedeEliminar));
                OnPropertyChanged(nameof(PuedeCrear));
                OnPropertyChanged(nameof(Contenido));
                OnPropertyChanged(nameof(Autor));
                OnPropertyChanged(nameof(Titulo));
                OnPropertyChanged(nameof(DocumentoExiste));
            }
        }

        public string RolActual
        {
            get => _rolActual;
            private set { _rolActual = value; OnPropertyChanged(); }
        }

        public List<string> Permisos
        {
            get => _permisosLista;
            private set { _permisosLista = value; OnPropertyChanged(); }
        }

        public string PermisosTexto => Permisos != null && Permisos.Count > 0 ? string.Join(", ", Permisos) : "Ninguno";

        public bool TieneAcceso => Permisos != null && Permisos.Contains("Ver");
        public bool PuedeEditar => Permisos != null && Permisos.Contains("Editar");
        public bool PuedeEliminar => Permisos != null && Permisos.Contains("Eliminar");
        public bool PuedeCrear => Permisos != null && Permisos.Contains("Crear");

        public string Titulo => _documentoReal?.Titulo ?? "No hay documento";

        public string Contenido => TieneAcceso ? _documentoReal?.Contenido ?? "No hay documento creado. Usa 'CREAR DOCUMENTO'" : "ACCESO DENEGADO - No tienes permisos para ver este contenido";

        public string Autor => TieneAcceso ? _documentoReal?.Autor ?? "Sin autor" : "ACCESO DENEGADO";

        public bool DocumentoExiste => _documentoReal != null;

        private void ActualizarPermisos()
        {
            if (_usuarioActual?.StartsWith("admin") == true)
                RolActual = "Admin";
            else if (_usuarioActual?.StartsWith("editor") == true)
                RolActual = "Editor";
            else if (_usuarioActual?.StartsWith("lector") == true)
                RolActual = "Lector";
            else
                RolActual = "Invitado";

            Permisos = _permisos.ContainsKey(RolActual) ? _permisos[RolActual] : new List<string>();
        }

        public void Mostrar() { }

        // Método para editar el documento
        public void EditarDocumento(string nuevoTitulo, string nuevoContenido, string nuevoAutor)
        {
            if (PuedeEditar && _documentoReal != null)
            {
                _documentoReal.Titulo = nuevoTitulo;
                _documentoReal.Contenido = nuevoContenido;
                _documentoReal.Autor = nuevoAutor;

                // Notificar cambios al proxy (importante para actualizar la UI)
                OnPropertyChanged(nameof(Titulo));
                OnPropertyChanged(nameof(Contenido));
                OnPropertyChanged(nameof(Autor));
            }
        }

        // Método para eliminar el documento
        public bool EliminarDocumento()
        {
            if (PuedeEliminar && _documentoReal != null)
            {
                _documentoReal = null;
                OnPropertyChanged(nameof(Titulo));
                OnPropertyChanged(nameof(Contenido));
                OnPropertyChanged(nameof(Autor));
                OnPropertyChanged(nameof(TieneAcceso));
                OnPropertyChanged(nameof(DocumentoExiste));
                return true;
            }
            return false;
        }

        // Método para crear un nuevo documento
        public bool CrearNuevoDocumento(string titulo, string contenido, string autor)
        {
            if (PuedeCrear)
            {
                _documentoReal = new DocumentoConfidencial(titulo, contenido, autor);
                OnPropertyChanged(nameof(Titulo));
                OnPropertyChanged(nameof(Contenido));
                OnPropertyChanged(nameof(Autor));
                OnPropertyChanged(nameof(TieneAcceso));
                OnPropertyChanged(nameof(DocumentoExiste));
                return true;
            }
            return false;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}