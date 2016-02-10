# include "FwsBinStaticFile.h"
# include "C:/Users/frodo/Documents/FwsVision/FwsVision.h"



int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsVGenDir("bin2.dat",FWS_RUTA_LOCAL);

    /*** crear y abrir el archivo para escritura e imprecines ***/
    FILE * archRegWr = FwsProdctCrtFle( rutaCompleta, 2);

    // escribir cabecera en el archivo
    FwsProdctPntHdr(archRegWr, 1012);

    // escribir datos de los productos


    // cerrar archivo para escritura binaria
    fclose( archRegWr);


    /***** abrir archivo para lectura e impreciones en pantalla *****/
    FILE * arcRegRd = FwsProdctCrtFle(rutaCompleta,1);

    // imprimir la cabecera en patalla
    printf("el archivo mide -> %d", FwsProdctRdHdr(arcRegRd) );





    return 0;
}
