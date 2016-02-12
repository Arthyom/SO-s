# include "FwsBinStaticFile.h"

int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);
    int hdr = 10;

    // iniciar el archivo
    FwsProdctInitFile(rutaCompleta,hdr);

    FwsProdctAgregar(hdr,rutaCompleta);
    FwsProdctMostrar(rutaCompleta);


    // acutlizar indices
    float s = 23.34;
    int a = 123;
    FwsProdctActlzr(rutaCompleta,3,3,&a);
    FwsProdctMostrar(rutaCompleta);








    return 0;
}
