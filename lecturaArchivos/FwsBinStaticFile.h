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

//*************> FUNCIONES MISELANEAS


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
            archivoRgst = fopen( ruta, "ab");
            if ( archivoRgst ){
                return archivoRgst;
            }
            else
                return NULL;
        break;

        case 4:
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
    return pVacio;
}

void            FwsProdctInitFile       ( char * rutaCompleta ){
    // crear un archivo nuevo
    FILE * arch = FwsProdctCrtFl(rutaCompleta,2);
    fclose(arch);
}

void            FwsProdctDsplyPrdct     ( FwsProdct * al ){
    // imprimir un producto indicado
    if (al->PrdctBandera !=0 )
     printf(" -> %d \t %s \t %f \t %d \t %d <- \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

void            FwsProdctRgst           ( FILE * archivoRegst, FwsProdct * nuevoPrdct ){
    // registrar el producto creado en el archivo de registro
    fwrite( nuevoPrdct, sizeof(FwsProdct), 1, archivoRegst);
}

void            FwsProdctDsplyHdr       ( FILE * archivoRegst ){
    // leer la canditadad de registros que contendra el archivo
    int dims ;
    fread( &dims,sizeof(int),1,archivoRegst);
    printf("%d \n",dims);

}

void            FwsProdctImprmrHdr      ( FILE * archivoRegst, int  dims){

    // imprimir el encavezado en el archivo binario
    fwrite( &dims,sizeof(int),1,archivoRegst );

}

void            FwsProdctImprmrPrdct    ( FILE *archivoRegistro, FwsProdct * pdctEntrada){
    // imprimir en el archivo los productos que se han ordenado
    fwrite( pdctEntrada, sizeof(FwsProdct), 1 , archivoRegistro);
}

void            FwsProdctDsplyPrdcts    ( FILE * archivoRgst,int hdr ){
    // imprimir en pantalla los registros del archivo
    fseek(archivoRgst, hdr+(1-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * al = FwsProdctCrtVd();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    while(!feof(archivoRgst)){
        FwsProdctDsplyPrdct(al);
        fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    }
}

void            FwsProdctDsplyDspz      ( FILE * archivoRgst, int indx, int hdr ){
    // mover el puntero hasta el registro[indx]
    /* se usa la formula */
    fseek(archivoRgst, hdr+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);

    // leer caracteres en esa posicion
    FwsProdct * lectura = FwsProdctCrtVd();
    fread( lectura, sizeof(FwsProdct), 1 , archivoRgst);
    FwsProdctDsplyPrdct(lectura);
}

FwsProdct   *   FwsProdctBscrPrdct      ( FILE * archivoRgst, int indx, int hdr ){
    // buscar un dice especifico y regresarlo
    fseek(archivoRgst, hdr+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * buscado = FwsProdctCrtVd();
    fread( buscado, sizeof(FwsProdct), 1 , archivoRgst);
    return buscado;
}

int             FwsProdctLgcElim        ( char * rutaCompleta,int indx, int hdr ){
    // eliminar un registro mediante eliminacion logica
    if ( FwsProdctVrfcrRng(indx,hdr)){

        // abrir archivo para lectura
        FILE * archLectura = FwsProdctCrtFl(rutaCompleta,1);
        // buscar indice
        FwsProdct * prdctElm = FwsProdctBscrPrdct(archLectura,indx,hdr);
        prdctElm->PrdctBandera = 1;
        prdctElm->PrdctId = 23;
        // cerrar archivo para lectura
        fclose(archLectura);

        // abrir archivo para escritura
        FILE * archEscritura = FwsProdctCrtFl(rutaCompleta,3);
        // navegar hasta la posicion indx
        fseek(archEscritura, hdr+(indx-1)*sizeof(FwsProdct) ,SEEK_END);
        // sobreescribir objeto en el archivo
        FwsProdctImprmrPrdct(archEscritura,prdctElm);
        fclose(archEscritura);

        return 1;
    }
    return 0;
}



#endif // FWSBINSTATICFILE_H




