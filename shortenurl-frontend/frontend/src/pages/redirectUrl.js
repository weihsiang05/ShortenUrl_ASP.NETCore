import * as React from 'react';
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';

const RedirectUrl = () => {
  //const [links, setLinks] = useState('')
  const [error, setError] = useState(null)
  const { randomLetter } = useParams();

  console.log(randomLetter)
  useEffect(() => {
    const fetchData = async () => {
      try {
        //const link = { links }
        const request = await fetch('/link/findRandomLetter', {
          method: 'POST',
          body: JSON.stringify({ randomLetter }),
          headers: {
            'Content-Type': 'application/json'
          }
        })

        const json = await request.json()
        //console.log(json.fullLink)

        if (!request.ok) {
          setError(json.error)
        } else {
          console.log(json)
          setError(null)
          // Redirect the user to the specified URL
          window.location.href = json.fullLink;
        }
      } catch (error) {
        setError(error)
      }
    }

    fetchData();
  }, [randomLetter]);

  return (
    <div>
      {error && <div className="Error">{error.toString()}</div>}
    </div>
  )
}



export default RedirectUrl;

