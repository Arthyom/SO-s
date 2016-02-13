# include "FwsBinStaticFile.h"


int main (){

    // concatenacion de ruta y nombre de archivo
    char * rutaCompleta = FwsProdctGnrtDir("bin2.bin",FWS_RUTA_LOCAL);
    int opcion,cnt = 0;

    do {
        FwsSttcsDdoble(21," TAKOS EL AZTECA ");
        opcion = FwsSttcsDmenu(6,"agregar", "eliminar","editar","mostar","ver encabezado","salir");
        cnt += FwsCall(opcion,rutaCompleta, cnt);
        FwsSttcsLimpiar(15,"precione ENTER para continuar");
    }while (opcion != 6);


    return 0;
}
