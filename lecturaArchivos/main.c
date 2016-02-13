# include "FwsBinStaticFile.h"


int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin1.bin",FWS_RUTA_LOCAL);
    int opcion;

    do {
        FwsSttcsDdoble(21," TAKOS EL AZTECA ");
        if ( FwsProdctVerArch(rutaCompleta)){
            opcion = FwsSttcsDmenu(6,"AGREGAR", "ELIMINAR","EDITAR","MOSTRAR","VER ENCAVEZADO","SALIR");
            FwsCall(opcion,rutaCompleta);
            FwsSttcsLimpiar(15,"precione ENTER para continuar");
        }
        else{
            opcion = FwsSttcsDmenu(6,"AGREGAR", "...","...","...","...","SALIR");
            FwsCall(opcion,rutaCompleta);
            FwsSttcsLimpiar(15,"precione ENTER para continuar");
        }
    }while (opcion != 6);


    return 0;
}
