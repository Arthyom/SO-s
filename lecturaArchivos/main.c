# include "FwsBinStaticFile.h"

int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);

    FwsProdctInitFile(rutaCompleta);

    FILE * archRegWr = FwsProdctCrtFl(rutaCompleta,3);
    // escribir cabecera en el archivo
    FwsProdctImprmrHdr(archRegWr,4);

    // crear un vector de objetos
    FwsProdct * p1 = FwsProdctCrtPrm(1,"taco",12,12,1);
    FwsProdct * p2 = FwsProdctCrtPrm(2,"taco",12,12,1);
    FwsProdct * p3 = FwsProdctCrtPrm(3,"taco",12,12,1);
    FwsProdct * p4 = FwsProdctCrtPrm(4,"taco",12,12,1);
    FwsProdct * p5 = FwsProdctCrtPrm(5,"taco",12,12,1);
    FwsProdct * p6 = FwsProdctCrtPrm(6,"taco",12,12,1);
    FwsProdct * p7 = FwsProdctCrtPrm(7,"taco",12,12,1);

    FwsProdctImprmrPrdcts(7,archRegWr,p1,p2,p3,p4,p5,p6,p7);


    // cerrar archivo para escritura binaria
    fclose( archRegWr);

    FILE * arcRegRd = FwsProdctCrtFl(rutaCompleta,1);
    //FwsProdctDsplyHdr(arcRegRd);
    FwsProdctDsplyPrdcts(arcRegRd,4);

    return 0;
}
