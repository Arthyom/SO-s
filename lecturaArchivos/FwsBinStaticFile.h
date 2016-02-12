#ifndef FWSBINSTATICFILE_H
#define FWSBINSTATICFILE_H

    /*************************************************************************/
    /*************************************************************************/
    /*****************     lectura de ficheros binarios     ******************/
    /*************************************************************************/
    /*************************************************************************/

    # include <stdio.h>
    # include <stdlib.h>
    # include <stdarg.h>
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
void            FwsProdctApuntar        ( FILE * archIn, int indx){
     fseek(archIn, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
}

int             FwsProdctVrfcrRng       ( int indx, int hdr ){
    if ( indx <= hdr )
        return 1;
    return 0;
}

char        *   FwsProdctGnrtDir        ( char * nombre, char * ruta ){

    int    dims         = strlen(nombre) + strlen(ruta);
    char * rutaCompleta = (char*) malloc( sizeof(char) * dims);
    strcpy(rutaCompleta,ruta);
    strcat(rutaCompleta,nombre);

    return rutaCompleta;
}

FILE        *   FwsProdctCrtFl          ( char * ruta, int modo ){
    // abrir el archivo segun la modalidad
    FILE * archivoRgst;
    switch ( modo ){

        case 1:
            // lectura binaria
            archivoRgst = fopen( ruta, "rb+");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;

        case 2:
            // escritura binaria
            archivoRgst = fopen( ruta, "wb+");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;

        case 3:
            // escritura binaria
            archivoRgst = fopen( ruta, "ab+");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;
    }
    return NULL;
}

FwsProdct   *   FwsProdctCrtPrm         ( int id, char * nombre, float precio, int stock, int bandera ){

    FwsProdct * nuevoPrdct = (FwsProdct*) malloc( sizeof(FwsProdct));
    nuevoPrdct->PrdctId     = id;
    nuevoPrdct->PrdctNombre = nombre;
    nuevoPrdct->PrdctPrecio = precio;
    nuevoPrdct->PrdctStock  = stock;
    nuevoPrdct->PrdctBandera = bandera;

    return nuevoPrdct;
}

FwsProdct   *   FwsProdctCrtVd          ( ){
    // crear un produto vacio
    FwsProdct * pVacio = (FwsProdct*) malloc(sizeof(FwsProdct));
    pVacio->PrdctId = 0;
    pVacio->PrdctBandera = 1;
    pVacio->PrdctNombre = NULL;
    pVacio->PrdctPrecio = 0;
    pVacio->PrdctStock = 0;
    return pVacio;
}

void            FwsProdctInitFile       ( char * rutaCompleta, int hdr ){
    // crear un archivo nuevo
    FILE * arch = FwsProdctCrtFl(rutaCompleta,2);
    FwsProdctImprmrHdr(arch,hdr);
    fclose(arch);
}

void            FwsProdctDsplyPrdct     ( FwsProdct * al ){
    // imprimir un producto indicado
    if (al->PrdctBandera !=0 )
     printf(" -> %d \t %s \t %f \t %d \t %d <- \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

void            FwsProdctDsplyHdr       ( char * ruta ){
    FILE * archivoRegst = FwsProdctCrtFl(ruta,1);
    // leer la canditadad de registros que contendra el archivo
    int dims ;
    fread( &dims,sizeof(int),1,archivoRegst);
    printf("%d \n",dims);
    fclose(archivoRegst);
}

void            FwsProdctImprmrHdr      ( FILE * archivoRegst, int  dims){
    // imprimir el encavezado en el archivo binario
    fwrite( &dims,sizeof(int),1,archivoRegst );
    fclose(archivoRegst);
}

void            FwsProdctImprmrPrdct    ( FILE *archivoRegistro, FwsProdct * pdctEntrada){
    // imprimir en el archivo los productos que se han ordenado
    fwrite( pdctEntrada, sizeof(FwsProdct), 1 , archivoRegistro);
}

void            FwsProdctDsplyPrdcts    ( FILE * archivoRgst ){
    // imprimir en pantalla los registros del archivo
    fseek(archivoRgst, sizeof(int) ,SEEK_SET);
    FwsProdct * al = FwsProdctCrtVd();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    while(!feof(archivoRgst)){
        FwsProdctDsplyPrdct(al);
        fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    }
    printf("vacio");
}

void            FwsProdctDsplyDspz      ( char * ruta, int indx ){

    FILE * archivoRgst = FwsProdctCrtFl(ruta,1);
    // mover el puntero hasta el registro[indx]
    /* se usa la formula */
    fseek(archivoRgst, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);

    // leer caracteres en esa posicion
    FwsProdct * lectura = FwsProdctCrtVd();
    fread( lectura, sizeof(FwsProdct), 1 , archivoRgst);
    fclose(archivoRgst);
    FwsProdctDsplyPrdct(lectura);
}

FwsProdct   *   FwsProdctBscrPrdct      ( FILE * archivoRgst, int indx ){
    // buscar un dice especifico y regresarlo
    fseek(archivoRgst, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * buscado = FwsProdctCrtVd();
    fread( buscado, sizeof(FwsProdct), 1 , archivoRgst);
    if (buscado)
        return buscado;

    return NULL;
}

FwsProdct   **  FwsProdctCrtVctrPrdct   ( int dims ){
    // crear un vector de productos vacios
    FwsProdct ** vector = (FwsProdct**) malloc(sizeof(FwsProdct*)* dims);
    int i ;
    for (i = 0 ; i < dims; i ++)
        vector[i] = FwsProdctCrtVd();

    return vector;
}


void            FwsProdctImprmrPrdcts   ( int dims, FILE * archRgst, ...){
    //agregar n objetos al archivo
    va_list listaPrm;
    va_start(listaPrm,archRgst);
    int i;
    for (i = 0 ; i < dims; i++ ){
        // sacer de la lista y agregar
        FwsProdct * pn = va_arg(listaPrm,FwsProdct *);
        FwsProdctImprmrPrdct(archRgst,pn);
    }

    va_end(listaPrm);
}


void            FwsProdctAgregar        ( int dims, char * ruta ){
    // abrir archivo para lectura
    FILE * fwrt = FwsProdctCrtFl(ruta,3);

    // crear obejts y agregarlos al archivo
    FwsProdct ** vect = FwsProdctCrtVctrPrdct(dims);
    int i;
    for( i = 0 ; i < dims ; i++ ){
        // capturar datos de los objetos
        vect[i]->PrdctId = i;

        // agregar al archivo
        FwsProdctImprmrPrdct(fwrt,vect[i]);

    }
    fclose(fwrt);
}


void            FwsProdctMostrar        ( char * ruta){
    // abrir archivo para lectura
    FILE * flect = FwsProdctCrtFl(ruta,1);
    // imprimir todos los productos
    FwsProdctDsplyPrdcts(flect);
    printf("\n");
    fclose(flect);
}


void            FwsProdctCloneFl        ( char * ruta, FILE * archVij ){
    // mover todos los datos del archivo viejo al nuevo
    FwsProdct * newProduct = FwsProdctCrtVd();

}


void            FwsProdctElimLG         ( char * ruta, int indx ){

    /*** buscar registro ***/

    // abrir el archivo para lectura
    FILE *archLect = FwsProdctCrtFl(ruta,1);

    // eliminar un registro con el indice indicado
    FwsProdct * rgstElim = FwsProdctBscrPrdct(archLect,indx);

    if(rgstElim){
        // cambiar bandera, ELIMINACION LOGICA
        rgstElim->PrdctBandera = 0 ;

        // cerrar el archivo para lectura
        fclose(archLect);


        /*** reescribir registro ***/
        // abrir el arcchivo para escritura
        FILE * archEscrt = FwsProdctCrtFl(ruta,1);

        // navegar a la posicion indicada
        FwsProdctApuntar(archEscrt,indx);

        // reescribir el registro eliminado
        fwrite(rgstElim,sizeof(FwsProdct),1,archEscrt);

        // cerrar el archivo
        fclose(archEscrt);
    }
    return NULL;
}


#endif // FWSBINSTATICFILE_H




