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



void            FwsProdctInitFile       ( char * rutaCompleta, int hdr, int modo ){
    // crear un archivo nuevo
    if (modo == 0){
    FILE * arch = FwsProdctCrtFl(rutaCompleta,2);
    FwsProdctImprmrHdr(arch,hdr);
    fclose(arch);
    }
    else{
        FILE * arch = FwsProdctCrtFl(rutaCompleta,1);
        FwsProdctImprmrHdr(arch,hdr);
        fclose(arch);
    }
}



void            FwsProdctDsplyPrdct     ( FwsProdct * al ){
    // imprimir un producto indicado
    if (al->PrdctBandera !=0 )
     printf(" \t -> %d \t\t %s \t\t %f \t\t %d \t\t %d  \n",al->PrdctId, al->PrdctNombre, al->PrdctPrecio, al->PrdctStock, al->PrdctBandera);
}




int             FwsProdctDsplyHdr       ( char * ruta ){
    FILE * archivoRegst = FwsProdctCrtFl(ruta,1);
    // leer la canditadad de registros que contendra el archivo
    int dims ;
    fread( &dims,sizeof(int),1,archivoRegst);
    fclose(archivoRegst);
    return dims;
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
    printf("\n \n");
    // imprimir en pantalla los registros del archivo
    fseek(archivoRgst, sizeof(int) ,SEEK_SET);
    FwsProdct * al = FwsProdctCrtVd();
    fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    while(!feof(archivoRgst)){
        FwsProdctDsplyPrdct(al);
        fread( al, sizeof(FwsProdct), 1 , archivoRgst);
    }
    printf("\n \n");
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



FwsProdct   *   FwsProdctBscrPrdct      ( char * ruta, int indx ){

    FILE * archivoRgst = FwsProdctCrtFl(ruta,1);
    // buscar un dice especifico y regresarlo
    fseek(archivoRgst, sizeof(int)+(indx-1)*sizeof(FwsProdct) ,SEEK_SET);
    FwsProdct * buscado = FwsProdctCrtVd();
    fread( buscado, sizeof(FwsProdct), 1 , archivoRgst);
    if (buscado && buscado->PrdctBandera != 0)
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

    FwsProdct * p = FwsProdctBscrPrdct(ruta,FwsProdctDsplyHdr(ruta));
    return p->PrdctId;
}


void            FwsProdctAgregar        ( int dims, char * ruta ){
    // abrir archivo para lectura
    FILE * fwrt = FwsProdctCrtFl(ruta,3);


    int nu = FwsProdctLastId(ruta);

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



void            FwsProdctActlzr         ( char * ruta, int indx, int modo, void * valor ){
    // cambiar el valor de un registro

    /// busca el registro indicado
    FILE *archLect = FwsProdctCrtFl(ruta,1);
    FwsProdct * update = FwsProdctBscrPrdct(archLect,indx);
    if (update->PrdctBandera == 0)
        return;
    char nm[30];
    if (update){
        // cambiar valores segun modo
        switch (modo){

            case 1:
                // cabiar nombre de regisro
                gets(nm);
                strcpy(update->PrdctNombre, nm);
            break;

            case 2:
                // cambiar el precio
                update->PrdctPrecio = *((float*) valor);
            break;

            case 3:
                // cambiar el stok
                update->PrdctStock = *((int*) valor);
            break;

           // actualizacion manual de todos los campos
           case 4: update = FwsProdctScnr(); break;

        }
        fclose(archLect);

        /*** comenzar la escritura de actualizacion ***/
        FILE *archRd = FwsProdctCrtFl(ruta,1);

        // apuntar a la poscion indicada
        FwsProdctApuntar(archRd,indx);

        // realizar escritura
        fwrite(update,sizeof(FwsProdct),1,archRd);

        fclose(archRd);
    }
}



void            FwsProdctGetActlz       ( char * ruta, int indx, int campo, char * nm, float prc, int stck ){
    switch (campo) {
        case 1: FwsProdctActlzr(ruta,indx,campo,nm);break;
        case 2: FwsProdctActlzr(ruta,indx,campo,&prc);break;
        case 3: FwsProdctActlzr(ruta,indx,campo,&stck);break;

        // actualizar todo el registo
        case 4:
             FwsProdctActlzr(ruta,indx,1,nm);
             FwsProdctActlzr(ruta,indx,2,&prc);
             FwsProdctActlzr(ruta,indx,3,&stck);
        break;
    }

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
    FILE * archLeer = FwsProdctCrtFl(ruta,1);
    int valor ;
    fread(&valor,sizeof(int),1,archLeer);
    fclose(archLeer);

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

        case 2 :   FwsSttcsDsingle(15,"SE HAN ELIMINADO LOS REGISTROS");  break;  break;

        case 3 :   FwsSttcsDsingle(13,"SE HAN ACTUALIZADO LOS REGISTROS");  break; break;



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


int             FwsProdctGetMode        (){

    FwsSttcsDdoble(21,"CAMPO A ACTUALIZAR");
    int campo = FwsSttcsDmenu(4,"NOMBRE","PRECIO","STOCK","TODOS");
    return campo;
}


int             FwsProdctVerArch        ( char * ruta ){
    FILE * arc = FwsProdctCrtFl(ruta,1);
    int n;
    if( fread(&n,sizeof(int),1,arc)){
        fclose(arc);
        return 1;
    }

        fclose(arc);
    return 0;
}

void            FwsCall                 ( int opcion, char * ruta ){

    int indx;
    int dims ;
    int hdr ;

    if ( FwsProdctVerArch(ruta)){
        //printf("no vacio");
         dims = FwsProdctDsplyHdr(ruta);
         hdr = dims;

        switch(opcion){
            case 1:
                dims = FwsProdctGetDims();
                FwsProdctAgregar(dims, ruta);
                FwsProdctActHdr(ruta,dims,2);
                FwsProdctOk(1);
            break;

            case 2:
               if (dims>0){
                   indx = FwsProdctGetIndx(hdr,1);
                   if (indx>0){
                       FwsProdctElimLG(ruta,indx);
                       FwsProdctActHdr(ruta,1,1);
                   }
                   else
                       return 0;
               }
               else{
                   FwsSttcsDsingle(17,"ERROR!!!!! ARCHIBO VACIO ");
                   return 0 ;
               }
               FwsProdctOk(2);
            break;

            case 3:
                if (dims>0){
                    indx = FwsProdctGetIndx(hdr,2);
                    // ver si el indice no ha sido borrado
                    FwsProdct * ps = FwsProdctBscrPrdct(ruta,indx);
                    if ( ps ){
                        int modo = FwsProdctGetMode();
                        switch(modo){
                            case 1: FwsProdctGetActlz(ruta,indx,FWS_PRDCT_NM,"0",0,0);break;
                            case 2: FwsProdctGetActlz(ruta,indx,FWS_PRDCT_PRC,0,*((float*) FwsProdctGetValue(2)),0);break;
                            case 3: FwsProdctGetActlz(ruta,indx,FWS_PRDCT_STCK,0,0,*((int*) FwsProdctGetValue(1)));break;
                            case 4: FwsProdctGetActlz(ruta,indx,FWS_PRDCT_TDS,"0",*((float*) FwsProdctGetValue(2)),*((int*) FwsProdctGetValue(1)));break;
                        }
                        FwsProdctOk(3);
                    }
                    else{
                        FwsSttcsDsingle(17,"ERROR NO EXISTE EL INDICE");
                    }
                }
                else{
                    FwsSttcsDsingle(17,"ERROR!!!!! ARCHIBO VACIO ");
                    return 0 ;}
            break;

            case 4:
                FwsProdctDsplyPrdcts(ruta);
            break;

            case 5:
                printf(" \t \t \t   -> %d \n",FwsProdctDsplyHdr(ruta) );
            break;
        }
    }
    else{
        //FwsSttcsDmenu(6,"agregar", "eliminar","editar","mostar","ver encabezado","salir");
        //printf("vacio");
        hdr = FwsProdctGetDims();
        dims = hdr;
        // iniciar el archivo
        FwsProdctInitFile(ruta,hdr,0);
        FwsProdctAgregar(hdr,ruta);
        FwsProdctOk(1);
    }
}


#endif // FWSBINSTATICFILE_H




