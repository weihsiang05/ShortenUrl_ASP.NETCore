import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Logo from "../logo/link_img.png";
import { Box } from '@mui/material';
import TextField from '@mui/material/TextField';
import { Link } from 'react-router-dom';
import { QRCode } from 'react-qr-code';
import { QrReader } from 'react-qr-reader';

import { useState } from 'react'
//import { use } from '../../../backend/routers/link';
//import { json } from 'react-router-dom';

const HomePage = () => {

  const [links, setLinks] = useState('')
  const [error, setError] = useState(null)
  const [url, setUrl] = useState('')
  const [originUrl, setoriginUrl] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault()

    const link = { links }

    //console.log("Links:", links);

    const respose = await fetch('/link', {
      method: 'POST',
      body: JSON.stringify(link),
      headers: {
        'Content-Type': 'application/json'
      }
    })



    const json = await respose.json()
    //console.log('Response:', json);
    console.log(json)

    if (!respose.ok) {
      setError(json.error)
      console.error('Error:', json.error);
    }
    //console.log("url" + url)
    if (respose.ok) {
      //console.log(json)
      setUrl(json.newLink)
      //console.log(json.newLink)
      setoriginUrl(json.fullLink)
      //console.log(json.fullLink)
      setLinks('')
      setError(null)
    }
  }

  const CopyToClipBoard = async () => {
    try {
      await navigator.clipboard.writeText(url);
      alert('URL copied to clipboard!');
    } catch (err) {
      setError(err)
    }
  }

  const handleScan = data => {
    if (data) {
      console.log('Result: ', data);
    }
  }

  const handleError = err => {
    console.error(err);
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
      <h2 style={{ marginBottom: '20px' }}>URL shortener</h2>
      <Box
        component="img"
        sx={{
          mr: 2,
          height: 150
        }}
        alt="logo"
        src={Logo}
        style={{ marginBottom: '20px' }}
      />
      <Box
        component="form"
        onSubmit={handleSubmit}
        sx={{
          '& > :not(style)': { m: 1, width: '25ch' },
        }}
        noValidate
        autoComplete="off"
      >
        {url ? (
          <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <Link to={originUrl}>{url}</Link>
            <Button variant="outlined" onClick={CopyToClipBoard} style={{ marginBottom: '20px' }}>
              Copy
            </Button>
            <QRCode value={originUrl} size={128} style={{ margin: '20px' }} />
            <QrReader
              delay={300}
              onError={handleError}
              onScan={handleScan}
              style={{ width: '50%' }}
            />
          </div>
        ) : (
          <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <TextField
              id="standard-basic"
              label="Input URL here"
              variant="standard"
              onChange={(e) => setLinks(e.target.value)}
              value={links}
            />
            <Stack spacing={2} direction="row">
              <Button type="submit" variant="outlined">
                Shorten
              </Button>
            </Stack>
          </div>
        )}

      </Box>
      {error && <div className="Error">{error}</div>}
    </div>
  )
}

export default HomePage;