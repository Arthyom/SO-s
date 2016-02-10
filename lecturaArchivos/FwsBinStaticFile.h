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

//*************> FUNCIONES MISELANEAS

char        *   FwsProdctGenDir ( char * nombre, char * ruta ){

    int    dims         = strlen(nombre) + strlen(ruta);
    char * rutaCompleta = (char*) malloc( sizeof(char) * dims);
    strcpy(rutaCompleta,ruta);
    strcat(rutaCompleta,nombre);

    return rutaCompleta;
}

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

void            FwsProdctRd1Prd ( FwsProdct * al ){
    // imprimir un producto indicado
    printf(" -> %d \t %s \t %f \t %d \t %d <- \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

void            FwsProdctRgst   ( FILE * archivoRegst, FwsProdct * nuevoPrdct ){
    // registrar el producto creado en el archivo de registro
    fwrite( nuevoPrdct, sizeof(FwsProdct), 1, archivoRegst);
}

void            FwsProdctRdHdr  ( FILE * archivoRegst ){
    // leer la canditadad de registros que contendra el archivo
    int dims ;
    fread( &dims,sizeof(int),1,archivoRegst);
    printf("%d \n",dims);

}

void            FwsProdctPnHdr  ( FILE * archivoRegst, int  dims){

    // imprimir el encavezado en el archivo binario
    fwrite( &dims,sizeof(int),1,archivoRegst );

}

void            FwsProdctPnPrd  ( FILE * archivoRegst, FwsProdct * pdctEntrada){
    // imprimir en el archivo los productos que se han ordenado
    fwrite( pdctEntrada, sizeof(FwsProdct), 1 , archivoRegst);
}

void            FwsProdctRdPrdS ( FILE * archivoRgst,int hdr ){
    // imprimir en pantalla los registros del archivo
    fseek(archivoRgst, hdr+(1-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * al = FwsProdctVdCr();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    while(!feof(archivoRgst)){
        FwsProdctRd1Prd(al);
        fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    }
}

void            FwsProdctRdDspz ( FILE * archivoRgst, int indx,int hdr){
    // mover el puntero hasta el registro[indx]
    /* se usa la formula */
    fseek(archivoRgst, hdr+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);

    // leer caracteres en esa posicion
    FwsProdct * lectura = FwsProdctVdCr();
    fread( lectura, sizeof(FwsProdct), 1 , archivoRgst);
    FwsProdctRd1Prd(lectura);
}

#endif // FWSBINSTATICFILE_H




