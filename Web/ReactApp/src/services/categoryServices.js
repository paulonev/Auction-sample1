import api from "../utils/apiLib";

const getAll = () => {
    return api
        .get("/category")
        .then((response) => {
            return {
                categories: response.data.categories
            }
        })
}

export default {
    getAll
}