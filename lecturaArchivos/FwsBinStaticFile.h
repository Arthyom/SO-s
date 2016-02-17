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
    # include "C:/Users/frodo/Documents/GitHub/FwsStetics/FwsStetics/FwsStetics.h"
   // # include "C:/Users/frodo/Documents/GitHub/FwsFile/FwsFile.h"

    # define FWS_RUTA_LOCAL "C:/Users/frodo/Desktop/"
    # define FWS_PRDCT_NM       1
    # define FWS_PRDCT_PRC      2
    # define FWS_PRDCT_STCK     3
    # define FWS_PRDCT_TDS      4

    /********************************************************/
    /*********    declarar el tipo WFSprodct     ************/
    /********************************************************/

    typedef struct{

        int         PrdctId;
        char        PrdctNombre[20];
        float       PrdctPrecio;
        int         PrdctStock;
        int         PrdctBandera;

    }FwsProdct;

    /********************************************************/
    /*********     prototipo y declaracion       ************/
    /********************************************************/

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


int             FwsProdctVerArch       ( char * ruta ){
    FILE * arc = FwsProdctCrtFl(ruta,1);
    int n;
    if( fread(&n,sizeof(int),1,arc)){
        fclose(arc);
        return 1;
    }

        fclose(arc);
    return 0;
}


void            FwsFileApntr        ( FILE * archIn, int indx){
     fseek(archIn, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
}

int             FwsProdctVrfcrRng       ( int indx, int hdr ){
    if ( indx <= hdr )
        return 1;
    return 0;
}




FwsProdct   *   FwsProdctCrtPrm         ( int id, char * nombre, float precio, int stock, int bandera ){

    FwsProdct * nuevoPrdct = (FwsProdct*) malloc( sizeof(FwsProdct));
    nuevoPrdct->PrdctId     = id;
    //nuevoPrdct->PrdctNombre = nombre;
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

    int i ;
    for (i=0; i < 20 ; i ++ )
        pVacio->PrdctNombre[i]='0';

    pVacio->PrdctPrecio = 0;
    pVacio->PrdctStock = 0;
    return pVacio;
}

FwsProdct   *   FwsProdctScnr           ( ){
    // escanear los valores de un producto
    FwsProdct * newPrdc = FwsProdctCrtVd();


    scanf("%s",&newPrdc->PrdctNombre);
    scanf("%f",&newPrdc->PrdctPrecio);
    scanf("%d",&newPrdc->PrdctStock);

    return newPrdc;
}



void            FwsProdctDsplyPrdct     ( FwsProdct * al ){
    // imprimir un producto indicado
    if (al->PrdctBandera !=0 )
     printf(" \t -> %d \t\t %s \t\t %f \t\t %d \t\t %d  \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

void            FwsProdctDsplyPrdctDl   ( FwsProdct * al ){
    // imprimir un producto indicado
    if (al->PrdctBandera ==0 )
     printf(" \t -> %d \t\t %s \t\t %f \t\t %d \t\t %d  \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}

int             FwsProdctGetHdr         ( char * ruta ){
    FILE * archivoRegst = FwsProdctCrtFl(ruta,1);
    int dims ;
    // leer la canditadad de registros que contendra el archivo
    if (fread( &dims,sizeof(int),1,archivoRegst)){
        fclose(archivoRegst);
        return dims;
    }

    return 0;
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

void            FwsProdctDsplyPrdcts    ( char * ruta ){
    FILE * archivoRgst = FwsProdctCrtFl(ruta,1);
    printf("");
    // imprimir en pantalla los registros del archivo
    fseek(archivoRgst, sizeof(int) ,SEEK_SET);
    FwsProdct * al = FwsProdctCrtVd();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    while(!feof(archivoRgst)){
        FwsProdctDsplyPrdct(al);
        fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    }
     printf("\n \n");
     FwsSttcsDSubLn(8," REGISTROS ELIMINADOS");
     fseek(archivoRgst, sizeof(int) ,SEEK_SET);
     fread( al, sizeof(FwsProdct), 1 , archivoRgst);
     while(!feof(archivoRgst)){
         FwsProdctDsplyPrdctDl(al);
         fread( al, sizeof(FwsProdct), 1 , archivoRgst);
     }
     fclose(archivoRgst);

}

int             FwsProdctFinalId        ( char * ruta ){

    FILE * arch = FwsProdctCrtFl(ruta,1);
    FwsProdct *anterior = FwsProdctCrtVd();
    FwsProdct *siguiente = FwsProdctCrtVd();


    fseek(arch, sizeof(int) ,SEEK_SET);
    // recorrer todo el archivo para encontrar el ultimo registro
    while( fread(siguiente,sizeof(FwsProdct),1,arch) ){
        anterior = siguiente;
        fread(siguiente,sizeof(FwsProdct),1,arch);
    }

    return siguiente->PrdctId;
}

FwsProdct   *   FwsProdctBscrPrdct      ( char * ruta, int indx ){

    FILE * archivoRgst = FwsProdctCrtFl(ruta,1);
    // buscar un dice especifico y regresarlo
    FwsProdct * buscado = FwsProdctCrtVd();
    fseek(archivoRgst, sizeof(int)+((indx-1)*sizeof(FwsProdct) ),SEEK_SET);
    fread( buscado, sizeof(FwsProdct), 1 , archivoRgst);
    fclose(archivoRgst);
    if (buscado && buscado->PrdctBandera != 0)
        return buscado;

    return NULL;
}

FwsProdct   *   FwsProdctBscrALLPrdct   ( char * ruta, int indx ){

    FILE * archivoRgst = FwsProdctCrtFl(ruta,1);
    // buscar un dice especifico y regresarlo
    fseek(archivoRgst, sizeof(indx)+((indx-1)*sizeof(FwsProdct) ),SEEK_SET);
    FwsProdct * buscado = FwsProdctCrtVd();
    fread( buscado, sizeof(FwsProdct), 1 , archivoRgst);
    fclose(archivoRgst);
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

int             FwsProdctLastId         ( char * ruta){

    FwsProdct * p = FwsProdctBscrPrdct(ruta,FwsProdctGetHdr(ruta));
    return p->PrdctId;
}



void            FwsProdctAgregar        ( int dims, char * ruta ){
    // abrir archivo para lectura
    FILE * fwrt = FwsProdctCrtFl(ruta,3);


    int nu = FwsProdctFinalId(ruta);

    // crear obejts y agregarlos al archivo
    FwsProdct ** vect = FwsProdctCrtVctrPrdct(dims);
    int i;
    for( i = 0 ; i < dims ; i++ ){
        // capturar datos de los objetos
        //vect[i] =FwsProdctScnr();
        vect[i]->PrdctId = i+1+nu;

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

void            FwsProdctElimLG         ( char * ruta, int indx ){

    /*** buscar registro ***/

    // abrir el archivo para lectura
    FILE *archLect = FwsProdctCrtFl(ruta,1);

    // eliminar un registro con el indice indicado
    FwsProdct * rgstElim = FwsProdctBscrALLPrdct(ruta,indx);

    if(rgstElim){
        // cambiar bandera, ELIMINACION LOGICA
        rgstElim->PrdctBandera = 0 ;

        // cerrar el archivo para lectura
        fclose(archLect);


        /*** reescribir registro ***/
        // abrir el arcchivo para escritura
        FILE * archEscrt = FwsProdctCrtFl(ruta,1);

        // navegar a la posicion indicada
        FwsFileApntr(archEscrt,indx);

        // reescribir el registro eliminado
        fwrite(rgstElim,sizeof(FwsProdct),1,archEscrt);

        // cerrar el archivo
        fclose(archEscrt);
    }
    return NULL;
}

int             FwsProdctActlzr         ( char * ruta, int indx, int modo){
    // cambiar el valor de un registro

    /// busca el registro indicado
    FILE *archLect = FwsProdctCrtFl(ruta,1);
    FwsProdct * update = FwsProdctBscrPrdct(ruta,indx);
    if (!update)
        return 1;
    char nm[30];
    int i;

    if (update){
        // cambiar valores segun modo
        switch (modo){

            case 1:
                // cabiar nombre de regisro
                scanf("%s",&nm);
                if ( 20 < strlen(nm))
                {
                    strcpy(update->PrdctNombre, nm);
                    for ( i = strlen(nm) - 1 ; i < 20 ; i++ )
                        nm[i]=' ';


                }
                else
                    strcpy(update->PrdctNombre, nm);


            break;

            case 2:
                // cambiar el precio
                scanf("%f",&update->PrdctPrecio);
            break;

            case 3:
                // cambiar el stok
                scanf("%d",&update->PrdctStock);
            break;

           // actualizacion manual de todos los campos
           case 4: update = FwsProdctScnr(); break;

        }
        fclose(archLect);

        /*** comenzar la escritura de actualizacion ***/
        FILE *archRd = FwsProdctCrtFl(ruta,1);

        // apuntar a la poscion indicada
        FwsFileApntr(archRd,indx);

        // realizar escritura
        fwrite(update,sizeof(FwsProdct),1,archRd);

        fclose(archRd);
    }

    return 0;
}



int             FwsProdctGetIndx        ( int hdr, int opc){
    // conseguir el indice
    int indx = 0;

    switch(opc){
        case 1:
            FwsSttcsDsingle(15,"seleccione el indice a [BORRAR]");
        break;

        case 2 :
            FwsSttcsDsingle(15,"seleccione el indice a [EDITAR]");
        break;
        case 3 :
            FwsSttcsDsingle(15,"seleccione el indice a [RESTAURAR]");
        break;
        case 4 :
            FwsSttcsDsingle(15,"seleccione el indice a [BUSCAR]");
        break;
    }
    scanf("%d",&indx);

    if ( indx > hdr || indx <= 0 ){
        FwsSttcsDsingle(10,"ERROR!! INDICE NO EXISTE INTENTE DE NUEVO");
        FwsProdctGetIndx(hdr,opc);
    }
    else
        return indx;
}

int             FwsProdctGetDims        ( ){
    FwsSttcsDsingle(15,"NUMERO DE REGISTROS A AGREGAR");
    int dims;
    scanf("%d",&dims);
    return dims;
}

void            FwsProdctActHdr         ( char * ruta, int nuevoValor,int indc ){

    int valor = FwsProdctGetHdr(ruta);


    FILE * archRd = FwsProdctCrtFl(ruta,1);
    switch(indc){
        case 1:
            valor -= nuevoValor;
            fwrite(&valor,sizeof(int),1,archRd);
            fclose(archRd);
        break;
        case 2 :
            valor += nuevoValor;
            fwrite(&valor,sizeof(int),1,archRd);
            fclose(archRd);
        break;
    }
}



void            FwsProdctOk             ( int modo ){
    switch(modo){

        case 1 :   FwsSttcsDsingle(15,"SE HAN AGREGADO LOS REGISTROS");  break;

        case 2 :   FwsSttcsDsingle(15,"SE HAN ELIMINADO LOS REGISTROS");  break;

        case 3 :   FwsSttcsDsingle(13,"SE HAN ACTUALIZADO LOS REGISTROS");  break;

        case 4 :   FwsSttcsDsingle(13,"SE HAN RESTAURADO  LOS REGISTROS");  break;



    }
}

void            FwsProdctBd             ( int modo ){
    switch(modo){

        case 1 :   FwsSttcsDdoble(15,"ERROR!!! ARCHIVO BASIO");  break;

        case 2 :   FwsSttcsDdoble(15,"ERROR!!! NO EXISTE INDICE");  break;

        case 3 :   FwsSttcsDdoble(13,"ERROR!!! NO SE PUEDE RESTAURAR");  break;

        case 4 :   FwsSttcsDdoble(13,"SE HAN RESTAURADO LOS REGISTROS");  break;

        case 5 :   FwsSttcsDdoble(13,"ERROR!!! OPERACION INVALIDA");  break;



    }
}

void    *       FwsProdctGetValue       ( int modo ){
    int a;
    float s;
    char p[20];

    switch (modo){
        case 1:scanf("%d",&a); return &a;
        case 2:scanf("%f",&s); return &s;
        case 3:scanf("%s",&p); return p;
    }
    return NULL;
}

int             FwsProdctGetMode        ( ){

    FwsSttcsDdoble(21,"CAMPO A ACTUALIZAR");
    int campo = FwsSttcsDmenu(4,"NOMBRE","PRECIO","STOCK","TODOS");
    return campo;
}



void            fwsRest                 ( char * rutaCompleta, int indx ){
    // restaurar el archivo indicado
    //FwsProdct * p1 = FwsProdctBscrPrdct(rutaCompleta, indx);
    FILE * f1 = FwsProdctCrtFl(rutaCompleta,1);
    /*
    fseek(f1, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * p1 = FwsProdctCrtVd();
    fread(p1,sizeof(FwsProdct),1,f1);
    */

    FwsProdct * p1 = FwsProdctBscrALLPrdct(rutaCompleta,indx);


    f1 = FwsProdctCrtFl(rutaCompleta,1);
    if ( p1 && p1->PrdctBandera == 0 ){
        p1->PrdctBandera = 1;

        fseek(f1, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
        fwrite(p1, sizeof(FwsProdct),1, f1 );
        FwsProdctOk(4);

    }
    else
        FwsProdctBd(3);

    fclose(f1);

}

void            FwsCall                 ( int opcion, char * ruta ){


    int indx;
    int dims ;
    int hdr ;
    int mode;

    if ( FwsProdctVerArch(ruta) ){
        hdr     =   FwsProdctGetHdr(ruta);
        dims    =   FwsProdctGetHdr(ruta);

        // opciones
        switch (opcion){


            // agregar
            case 1:
                dims = FwsProdctGetDims();
                FwsProdctAgregar(dims, ruta);
                FwsProdctActHdr(ruta,dims,2);
                FwsProdctOk(1);
            break;

            // eliminar
            case 2:

                if( FwsProdctGetHdr(ruta) ){
                    indx = FwsProdctGetIndx(hdr,1);
                    FwsProdctElimLG(ruta,indx);
                    FwsProdctActHdr(ruta,1,1);

                }
                else
                    return;
            break;

            case 3:
                // actualizar
                indx = FwsProdctGetIndx(hdr,2);
                mode = FwsProdctGetMode();
                if (  !FwsProdctActlzr(ruta,indx,mode) )
                    FwsProdctOk(3);
                else
                    FwsProdctBd(2);
            break;

            case 4:
                // mostrar registros
                FwsSttcsDSubLn(8,"  Id                     Nombre                   Precio            Existencia        Estado");
                FwsProdctDsplyPrdcts(ruta);
            break;

            case 5:
                // restaurar un archivo
                indx = FwsProdctGetIndx(hdr,3);
                fwsRest(ruta,indx);
                FwsProdctActHdr(ruta,1,2);

            break;

            case 6:
                printf(" \t \t \t   -> %d \n",FwsProdctGetHdr(ruta) );
            break;

            case 7:
                indx = FwsProdctGetIndx(hdr,4);
                FwsProdct * p = FwsProdctBscrPrdct(ruta,indx);
                if (p){
                    FwsSttcsDSubLn(8,"  Id                     Nombre                   Precio            Existencia        Estado");
                    FwsProdctDsplyPrdct(p);
                    FwsSttcsDSalto(2);
                }
                else
                    FwsProdctBd(2);

            break;
        }
    }
    else
    {
        if (opcion == 8)
            return;

        if ( opcion == 1 ){
            // el archivo esta vacio,  se debe iniciar
            hdr = FwsProdctGetDims();
            dims = hdr;
            FILE * f = FwsProdctCrtFl(ruta,2);
            FwsProdctImprmrHdr( f, dims);
            fclose(f);
            FwsProdctAgregar(hdr,ruta);
            FwsProdctOk(1);
        }
        else
            FwsProdctBd(5);

    }
}

int             FwsProdctExstFl         ( char * rutaCompleta ){

    int opcion = FwsSttcsDmenu(8,"AGREGAR", "ELIMINAR","EDITAR","MOSTRAR","RESTAURAR","VER ENCAVEZADO","BUSCAR","SALIR");
    FwsCall(opcion,rutaCompleta);
    FwsSttcsLimpiar(15,"precione ENTER para continuar");
    return opcion;
}

int             FwsProdctNtExstFl       ( char * rutaCompleta ){
    int opcion = FwsSttcsDmenu(8,"AGREGAR", "...","...","..." ,"...","...","...","SALIR");
    FwsCall(opcion,rutaCompleta);
    FwsSttcsLimpiar(15,"precione ENTER para continuar");
    return opcion;
}

#endif // FWSBINSTATICFILE_H




