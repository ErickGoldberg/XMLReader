
const AxiosConection = axios.create({
    BASEURL: `http://localhost:5071`,
    headers: {
        "Content-Type": "application/json",
        'Access-Control-Allow-Origin': "*"
    }
})

export default AxiosConection;