#ifndef FWSBINSTATICFILE_H
#define FWSBINSTATICFILE_H

    /*************************************************************************/
    /*************************************************************************/
    /*****************     lectura de ficheros binarios     ******************/
    /*************************************************************************/
    /*************************************************************************/

    # include <stdio.h>
    # include <stdlib.h>
    # include <string.h>

    # define FWS_RUTA_LOCAL "C:/Users/frodo/Desktop/"

    /********************************************************/
    /*********    declarar el tipo WFSprodct     ************/
    /********************************************************/

    typedef struct{

        int         PrdctId;
        char    *   PrdctNombre;
        float       PrdctPrecio;
        int         PrdctStock;
        int         PrdctBandera;

    }FwsProdct;

    /********************************************************/
    /*********     prototipo y declaracion       ************/
    /********************************************************/

FILE        *   FwsProdctCrtFle ( char * ruta, int modo ){
    // abrir el archivo segun la modalidad
    FILE * archivoRgst;
    switch ( modo ){

        case 1:
            // lectura binaria
            archivoRgst = fopen( ruta, "rb");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;

        case 2:
            // escritura binaria
            archivoRgst = fopen( ruta, "wb");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;

        case 3:
            // escritura binaria
            archivoRgst = fopen( ruta, "wb+");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;
    }
    return NULL;
}

FwsProdct   *   FwsProdctCrear  ( int id, char * nombre, float precio, int stock, int bandera ){

    FwsProdct * nuevoPrdct = (FwsProdct*) malloc( sizeof(FwsProdct));
    nuevoPrdct->PrdctId     = id;
    nuevoPrdct->PrdctNombre = nombre;
    nuevoPrdct->PrdctPrecio = precio;
    nuevoPrdct->PrdctStock  = stock;
    nuevoPrdct->PrdctBandera = bandera;

    return nuevoPrdct;
}

FwsProdct   *   FwsProdctVdCr   ( ){
    // crear un produto vacio
    FwsProdct * pVacio = (FwsProdct*) malloc(sizeof(FwsProdct));
    return pVacio;
}

void            FwsProdctRgst   ( FILE * archivoRegst, FwsProdct * nuevoPrdct ){
    // registrar el producto creado en el archivo de registro
    fwrite( nuevoPrdct, sizeof(FwsProdct), 1, archivoRegst);
}


int             FwsProdctRdHdr  ( FILE * archivoRegst ){
    // leer la canditadad de registros que contendra el archivo
    int dims ;
    fread( &dims,sizeof(int),1,archivoRegst);
    return dims;
}

void            FwsProdctPntHdr ( FILE * archivoRegst, int  dims){

    // imprimir el encavezado en el archivo binario
    fwrite( &dims,sizeof(int),1,archivoRegst );

}

void            FwsProdctPntPrd ( FILE * archivoRegst, FwsProdct * pdctEntrada){
    // imprimir en el archivo los productos que se han ordenado
    fwrite( pdctEntrada, sizeof(FwsProdct), 1 , archivoRegst);
}

void            FwsProdctRdPrd  ( FILE * archivoRgst ){
    // imprimir en pantalla los registros del archivo
    FwsProdct * al = FwsProdctVdCr();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    printf(" -> %d \t %s \t %f \t %d \t %d <-",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

#endif // FWSBINSTATICFILE_H
