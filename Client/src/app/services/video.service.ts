import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class VideoService {
  // Get YouTube video thumbnail
  getYouTubeThumbnail(url: string): string {
    const videoId = this.extractYouTubeVideoId(url);
    if (videoId) {
      return `https://img.youtube.com/vi/${videoId}/hqdefault.jpg`;
    }
    return 'assets/video-placeholder.jpg'; // Fallback placeholder
  }

  // Extract YouTube video ID from different URL formats
  private extractYouTubeVideoId(url: string): string | null {
    if (!url) return null;

    let videoId = null;

    try {
      // Standard YouTube watch URL
      if (url.includes('youtube.com/watch')) {
        const urlParams = new URLSearchParams(new URL(url).search);
        videoId = urlParams.get('v');
      }
      // Shortened youtu.be URL
      else if (url.includes('youtu.be/')) {
        videoId = url.split('youtu.be/')[1].split(/[?&#]/)[0];
      }
      // Embed URL
      else if (url.includes('youtube.com/embed/')) {
        videoId = url.split('youtube.com/embed/')[1].split(/[?&#]/)[0];
      }
    } catch (e) {
      console.error('Invalid YouTube URL:', url);
    }

    return videoId;
  }
}
