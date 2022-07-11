import { get, post } from "./common/http";
import { BASE_URL } from "./common/const";

export const GetToDoDetail = (params: any) =>
  get(`${BASE_URL}/api/ToDo/GetDetail?` + params, null);

export const GetToDoList = (params: any) =>
  get(`${BASE_URL}/api/ToDo/GetList?` + params, null);

export const CreateToDo = (data: any) =>
  post(`${BASE_URL}/api/ToDo/Create`, data);

export const UpdateCompletedStatus = (data: any) =>
  post(`${BASE_URL}/api/ToDo/UpdateCompletedStatus`, data);

export const DeleteToDo = (data: any) =>
  post(`${BASE_URL}/api/ToDo/Delete`, data);
  