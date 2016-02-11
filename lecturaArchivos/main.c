# include "FwsBinStaticFile.h"

int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);

    FwsProdctInitFile(rutaCompleta);

    FILE * archRegWr = FwsProdctCrtFl(rutaCompleta,3);
    // escribir cabecera en el archivo
    FwsProdctImprmrHdr(archRegWr,4);

    // escribir datos de los productos
    FwsProdct * p1 = FwsProdctCrtPrm(1,"taco",12.4,10,1);
    FwsProdct * p2 = FwsProdctCrtPrm(2,"buro",10.9,14,1);
    FwsProdct * p3 = FwsProdctCrtPrm(3,"kezo",40.2,62,1);
    FwsProdct * p4 = FwsProdctCrtPrm(4,"zope",45.2,94,1);
    FwsProdctImprmrPrdct( archRegWr, p1);
    FwsProdctImprmrPrdct( archRegWr, p2);
    FwsProdctImprmrPrdct( archRegWr, p3);
    FwsProdctImprmrPrdct( archRegWr, p4);


    // cerrar archivo para escritura binaria
    fclose( archRegWr);


    // eliminar un registro
    FwsProdctLgcElim(rutaCompleta, 2, 4);

    /***** abrir archivo para lectura e impreciones en pantalla *****/
    FILE * arcRegRd = FwsProdctCrtFl(rutaCompleta,1);
    //FwsProdctDsplyHdr(arcRegRd);
    FwsProdctDsplyPrdcts(arcRegRd,4);

    return 0;
}
