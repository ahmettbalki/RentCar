export interface Result<T>{
    data?: T;
    errorMesage?: string[];
    isSuccessfull: boolean;
    statusCode: number;
}