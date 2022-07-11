import React, { useContext } from "react";

let token = "";

export const post = (url: string, data = {}, fileToken = "") =>

  new Promise((resolve, reject) => {
    
    let options = {
      headers: {
        Authorization: token ? `Bearer ${token}` : "",
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    };

    let postOptions = { ...options.headers };

    if (fileToken.length > 0) {
      postOptions = {
        ...options.headers,
        Authorization: fileToken ? `Bearer ${fileToken}` : "",
      };
    }

    return (
      fetch(url, {
        method: "POST",
        headers: postOptions,
        body: JSON.stringify(data),
      })
        .then((response) =>
          response.json().then((body) => (
            {
              ok: response.ok,
              status: response.status,
              statusText: response.statusText,
              data: body,
            }))
        )
        .then((responseJson) => {
          if (!responseJson.ok) {
            if (responseJson.status === 400) {
              //400 = bad request
              if (responseJson.data && responseJson.data.message)
                throw responseJson.data.message;
              else throw responseJson.statusText;
            } else throw responseJson.statusText;
          } else resolve(responseJson.data);
        })
        .catch((error) => {
          reject(error);
        })
    );
  });
export const get = (url:any, params:any) =>
new Promise((resolve, reject) => {    
      let options = {
        headers: {

          Authorization: token ? `Bearer ${token}` : "",
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      };
      return fetch(url, { method: "GET",...options})
        .then((response) =>
          response.json().then((body) => ({
            ok: response.ok,
            status: response.status,
            statusText: response.statusText,
            data: body,
          }))
        )
        .then((responseJson) => {
          resolve(responseJson);
          if (!responseJson.ok) {
            if (responseJson.status === 400) {
              //400 = bad request
              if (responseJson.data && responseJson.data.message)
                throw responseJson.data.message;
              else throw responseJson.statusText;
            } else throw responseJson.statusText;
          } else resolve(responseJson.data);
        })
        .catch((error) => {
          reject(error);
        });
    });


