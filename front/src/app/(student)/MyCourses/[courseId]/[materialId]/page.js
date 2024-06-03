'use client';

import React, { useContext, useEffect, useState } from 'react';
import ViewTask from '../../../components/View/ViewTask';
import ViewAnnouncement from '../../../components/View/ViewAnnouncement';
import ViewFile from '../../../components/View/ViewFile';
import ViewLink from '../../../components/View/ViewLink';
import { useParams } from 'next/navigation';
import Layout from '../../../studentLayout/Layout';
import axios from 'axios';
import { UserContext } from '../../../../../context/user/User';

export default function Page() {
  const { userToken } = useContext(UserContext);
  const [type, setType] = useState();
  const [name, setName] = useState();
  const [error, setError] = useState(null);

  const { materialId, courseId } = useParams();

  const getMaterial = async () => {
    try {
      const { data } = await axios.get(
        `https://localhost:7116/api/MaterialControllar/GetMaterialById?id=${materialId}`,
        { headers: { Authorization: `Bearer ${userToken}` } }
      );

      setType(data.result.type);
      setName(data.result.name);
    } catch (err) {
      setError(err);
    }
  };

  useEffect(() => {
    getMaterial();
  }, [userToken]);

  const resetError = () => {
    setError(null);
    getMaterial();
  };

  if (error) {
    return (
     <div>
        <h2>Something went wrong!</h2>
        <p>{error.message}</p>
        <button onClick={resetError}>Try again</button>
      </div>
    );
  }

  return (
    <Layout title={name}>
      {type === 'Task' && <ViewTask materialID={materialId} type="courseId" Id={courseId} />}
      {type === 'Announcement' && <ViewAnnouncement materialID={materialId} type="courseId" Id={courseId} />}
      {type === 'File' && <ViewFile materialID={materialId} type="courseId" Id={courseId} />}
      {type === 'Link' && <ViewLink materialID={materialId} type="courseId" Id={courseId} />}
    </Layout>
  );
}
