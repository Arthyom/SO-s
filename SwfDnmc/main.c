#include "FwsDinamic.h"


int main(void)
{
    // crear el archivo
    char * ruta = FwsProdctGnrtDir("bin0.bin", FWS_RUTA_LOCAL);

    // agregar valores
    //fwsDinmcFileImprmr(ruta);


    fwsDinmcFileImprmrAlt(ruta);
    fwsDinmcFileDiplyArchAlt(ruta);

    fwsDinmcFileGet(ruta);

    // ver los registros
    //fwsDinmcFileDiplyArch(ruta);

    //fwsDinmcFileDiplyArchAlt(ruta);














    return 0;
}

