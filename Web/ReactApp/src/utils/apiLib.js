import Axios from "axios";

const api = Axios.create({
    baseURL: "https://localhost:5001/api",
    withCredentials: true,
});

export const setInterceptor = (setError) => {
    api.interceptors.response.use(
        (response) => {setError(null); return response;},
        (error) => {
            if (error.response) {
                if (error.response.status === 400) {
                    console.log("Response 400 called");                    
                    setError(error.response.data);
                    return Promise.reject(error.response.data);
                }
            }
        }
    )
}

export default api;