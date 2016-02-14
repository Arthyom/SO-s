# include "FwsBinStaticFile.h"


int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin0.bin",FWS_RUTA_LOCAL);
    int opcion;

    do
    {
        FwsSttcsDdoble(21," TAKOS EL AZTECA ");

        if ( FwsProdctVerArch(rutaCompleta) )
            opcion = FwsFileExstFl(rutaCompleta);
        else
            opcion = FwsFileNtExstFl(rutaCompleta);

    }while (opcion != 6);

    return 0;
}
