# include "FwsBinStaticFile.h"

int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);
    int hdr = 10;

    // iniciar el archivo
    FwsProdctInitFile(rutaCompleta,hdr);

    FwsProdctAgregar(hdr,rutaCompleta);
    FwsProdctMostrar(rutaCompleta);




    FwsProdctGetActlz(rutaCompleta,2,FWS_PRDCT_TDS,"nuevo nombre",1.12,32);
    FwsProdctMostrar(rutaCompleta);








    return 0;
}
