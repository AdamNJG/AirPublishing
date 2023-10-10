'use client';

import React, { ChangeEvent, useState, useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid';
import restClient from './scripts/restClient';
import Image from 'next/image';

interface FileDetails {
  userId: string;
  fileName: string | null;
  imageData: string | null;
}

export default function Home () {
  const [ fileDetails, setFileDetails ] = useState<FileDetails>({userId: '', fileName: null, imageData: null});
  const [ image, setImage ] = useState<string>('');

  useEffect(() => {
    setFileDetails({ userId: uuidv4().toString(), fileName: '', imageData: '' });
  },[setFileDetails]);

  useEffect(()=> {
    if (!fileDetails.fileName || fileDetails.fileName.length === 0 || !fileDetails.imageData || fileDetails.imageData.length === 0) {
      return;
    }
    restClient.postImage(fileDetails.userId, fileDetails.imageData, fileDetails.fileName)
      .then(response => {
        if (response) {
          const pathParts = response.data.split('\\');
          setImage(`/${pathParts[pathParts.length -1]}`);
        }
      });
  },[fileDetails]);

  function handleOnChange (event: ChangeEvent<HTMLInputElement>) {
    if (!event.target.files) {
      return;
    }
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      let fileData;
      reader.onloadend = () => {
        fileData = reader.result as string;
        if (fileData) setFileDetails({...fileDetails, fileName: file.name, imageData: fileData.split(',')[1]});
      };

      reader.readAsDataURL(file);
    }
  }

  return (
    <main style={{backgroundColor: 'grey'}}>
      <input
        id="image"
        type="file"
        name="image"
        onChange={handleOnChange}
      />
      {image !== '' ? <Image src={image} width='500' height='500' alt='uploaded picture preview' priority/>: null}
    </main>
  );
}
 