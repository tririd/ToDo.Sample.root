

function getEnvironment() {

    return {
        envName: "LOCAL",
        dbUrl: "http://localhost:17533",
        pagesize: 10,
    };

}

let appEnv = getEnvironment();

export const BASE_URL = appEnv?.dbUrl;
export const PAGE_SIZE = appEnv?.pagesize;

