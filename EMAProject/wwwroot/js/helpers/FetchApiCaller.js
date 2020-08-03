export default class FetchApiCaller {

    async callGet(url) {
        const response = await fetch(url)
            .then(this.handleErrors)
            .then(function (response) {
                return response;
            })
            .catch((error) => {
                console.log(error)
            })
      
        return response.json();
    }

    async callPost(url, data) {
        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data)
        })
            .then(this.handleErrors)
            .then(response => response.json())
            .catch(error => console.log(error));

        return response; 
    }

    handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }
}