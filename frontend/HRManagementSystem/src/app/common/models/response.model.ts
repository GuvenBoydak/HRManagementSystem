export  class ResponseModel<T>{
  data: T;
  errorMessages: string[];
  statusCode: number;
  isSuccess: boolean;
  isFailure: boolean;
  urlAsCreated: string;
}
