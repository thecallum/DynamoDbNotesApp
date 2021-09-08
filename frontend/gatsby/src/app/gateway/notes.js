import { NOTES_ENDPOINT } from "../constants";

export const getAllNotes = () => {
    const endpoint = NOTES_ENDPOINT;

    return new Promise(resolve => {

        fetch(endpoint)
            .then(response => {
                if (response.status !== 200) resolve(null);

                return response.json()
            })
            .then(response => {
                resolve(response)
            })
            .catch(err => {
                console.error(err)
                resolve(null)
            })



    })
}

export const getNoteById = (id) => {
    const endpoint = NOTES_ENDPOINT + id;

    return new Promise(resolve => {
        fetch(endpoint)
            .then(response => {
                if (response.status !== 200) resolve(null);

                return response.json()
            })
            .then(response => {
                resolve(response)
            })
            .catch(err => {
                console.error(err)
                resolve(null)
            })
    })
}

export const createNote = (newNote) => {
    const endpoint = NOTES_ENDPOINT;

    return new Promise(resolve => {
        fetch(endpoint, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newNote)
        })
            .then(response => {
                if (response.status !== 201) resolve(null);

                return response.json()
            })
            .then(response => {
                resolve(response)
            })
            .catch(err => {
                console.error(err)
                resolve(null)
            })
    })
}

export const updateNote = async (id, newNote) => {
    const endpoint = NOTES_ENDPOINT + id;

    return new Promise(resolve => {
        fetch(endpoint, {
            method: "PATCH",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newNote)
        })
            .then(response => {
                if (response.status !== 200) resolve(null);

                resolve(true)
            })
            .catch(err => {
                console.error(err)
                resolve(null)
            })
    })
}

export const deleteNote = (id) => {
    const endpoint = NOTES_ENDPOINT + id;

    return new Promise(async resolve => {
        try {
            const response = await fetch(endpoint, { method: "DELETE" });

            if (response.status === 200) resolve(true);

            resolve(false);

        } catch (e) {
            console.error(e);
            resolve(false);
        }
    })
}