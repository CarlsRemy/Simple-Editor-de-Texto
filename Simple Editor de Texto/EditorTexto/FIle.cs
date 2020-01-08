using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EditorTexto
{
    class FileOperations : IFileE_S
    {

        private string __strPath;
        private FileStream __FSFile;
        private bool _Flag;

        //Determina si el archivo se abrio
        public bool Flag
        {
            get { return _Flag; }
        }

        // La ruta del archivo
        public string Path
        {
            get{
                if (__strPath != "")
                    return __strPath;
                else
                    return " ";
            }
            set { __strPath = value; }
        }
     
        //Escribe un contenido en un texto
        public void WriteFile(string Text)
        {
            // Boramos el archivo 
            File.Delete(this.Path);

            // Abre el archivo
            this.OpenFile();
            if(__FSFile != null)
            {
                try
                {
                    //Se crear el buffer para escribir el archivo
                    byte [] buffer = Encoding.UTF8.GetBytes(Text);
                    __FSFile.Write(buffer, 0, Encoding.UTF8.GetByteCount(Text));
                }
                catch
                {
                    
                }

            }
            __FSFile.Close();
        }

        //Lee un archivo retorna su contenido
        public string ReadFile()
        {
                string ContentFile = "";
                // Abre el archivo
                this.OpenFile();
                if(__FSFile != null)
                {
                    //Bytes contiene el archivo
                    int LengthFileContent = (int)__FSFile.Length;
                    int count, sum = 0;
                    //Se crea el buffer para leer
                    byte[] Buffer = new byte[LengthFileContent];
                    while((count = __FSFile.Read(Buffer
                        ,sum, LengthFileContent-sum))>0)
                    {
                        sum +=count;
                    // se parsea los bytes a texto
                        ContentFile += Encoding.ASCII.GetString(Buffer);

                    }
                    // si establece verdado(ya se a leido el archivo)
                    _Flag = true;
                }
               
                __FSFile.Close();
                return ContentFile;
        }

        // Abre un archivo
        private void  OpenFile()
        {
            // Si exite solo lo abre
             if (File.Exists(this.Path))
             {
                __FSFile = new FileStream(this.__strPath,
                   FileMode.Open,
                   FileAccess.ReadWrite);
             }
             // Si no lo crea
              else
              {
                __FSFile = new FileStream(this.__strPath,
                        FileMode.CreateNew,
                        FileAccess.ReadWrite);
              }
        }
     
        public FileOperations()
        {
            this._Flag = false;
            this.__FSFile = null;
        }
    }
}
