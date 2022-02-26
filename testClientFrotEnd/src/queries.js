export class Queries{
    static create(query){
       return fetch('https://localhost:44353/ip/location?ip=',{
            method: 'GET',
            body: JSON.stringify(query),
            headers:{
                'Content-Type':'application/json'
            }
        })
            .then(response=> response.json())
            .then(response => {
                console.log(response)
            })
    }
}