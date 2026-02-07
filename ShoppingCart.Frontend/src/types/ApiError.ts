export interface ApiError {
  errorCode: string;
  message: string;
  invalidCartItemIds?: number[];
  status: number;
}
