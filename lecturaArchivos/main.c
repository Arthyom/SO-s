# include "FwsBinStaticFile.h"
# include "C:/Users/frodo/Documents/GitHub/FwsVision/FwsVision.h"



int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGenDir("bin2.bin",FWS_RUTA_LOCAL);

    /*** crear y abrir el archivo para escritura e imprecines ***/
    FILE * archRegWr = FwsProdctCrtFle( rutaCompleta, 2);

    // escribir cabecera en el archivo
    FwsProdctPnHdr(archRegWr,4);

    // escribir datos de los productos
    FwsProdct * p1 = FwsProdctCrear(4,"taco",12.4,10,0);
    FwsProdct * p2 = FwsProdctCrear(3,"buro",10.9,14,1);
    FwsProdct * p3 = FwsProdctCrear(1,"kezo",40.2,62,0);
    FwsProdct * p4 = FwsProdctCrear(5,"zope",45.2,94,1);
    FwsProdctPnPrd( archRegWr, p1);
    FwsProdctPnPrd( archRegWr, p2);
    FwsProdctPnPrd( archRegWr, p3);
    FwsProdctPnPrd( archRegWr, p4);


    // cerrar archivo para escritura binaria
    fclose( archRegWr);


    /***** abrir archivo para lectura e impreciones en pantalla *****/
    FILE * arcRegRd = FwsProdctCrtFle(rutaCompleta,1);
    //FwsProdctRdHdr(arcRegRd);
    //FwsProdctRdPrdS(arcRegRd,4);

    // navegar hasta un registro
    FwsProdctRdDspz(arcRegRd,4,4);




    return 0;
}
