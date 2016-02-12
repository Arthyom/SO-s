# include "FwsBinStaticFile.h"

int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);
    int hdr = 10;

    // iniciar el archivo
    FwsProdctInitFile(rutaCompleta);
    FwsProdctImprmrHdr(rutaCompleta,hdr);
    FwsProdctDsplyHdr(rutaCompleta);
    FwsProdctAgregar(hdr,rutaCompleta);
    FwsProdctMostrar(rutaCompleta);












    return 0;
}
